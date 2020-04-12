using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GamingReviews.ViewModels
{
    class GamePageViewModel : BaseViewModel
    {
        List<Games> games;
        
        public List<Games> Games
        {
            get
            {
                if (games == null)
                {
                    using(var unitOfWork = new UnitOfWork(new DBContext()))
                    {
                        
                        if (!unitOfWork.Games.Any())
                        {
                            unitOfWork.Games.Add(new Games("Bethesda", "Fallout", 1,new byte[2]));

                            unitOfWork.Complete();

                            games = unitOfWork.Games.GetAll() as List<Games>;

                        }
                        else
                            games = unitOfWork.Games.GetAll() as List<Games>;
                        NotifyPropertyChanged("Games");
                    }
                }
                return games;
            }
        }

        ICommand goToHomePage;
        ICommand goToUserPage;

        public ICommand GoToHomePage
        {
            get
            {
                if (goToHomePage == null)
                {
                    goToHomePage = new RelayCommand<Object>(GoToHomeScreen);
                }
                return goToHomePage;
            }
        }

        public ICommand GoToUserPage
        {
            get
            {
                if (goToUserPage == null)
                {
                    goToUserPage = new RelayCommand<Object>(GoToUserScreen);
                }
                return goToUserPage;
            }
        }

        private void GoToHomeScreen()
        {
            App.Current.MainWindow.Content = ViewModelsFactory.ViewModelType(ViewModelTypes.HomePageViewModel);
        }

        private void GoToUserScreen()
        {
            App.Current.MainWindow.Content = ViewModelsFactory.ViewModelType(ViewModelTypes.UserPageViewModel);
        }

    }
}
