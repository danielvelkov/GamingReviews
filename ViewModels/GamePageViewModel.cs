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
                    using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                    {

                        if (!unitOfWork.Games.Any())
                        {
                            unitOfWork.Games.Add(new Games("Bethesda", "Fallout", 1, new byte[2]));

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

        public ICommand GoToUserPage
        {
            get
            {
                if (goToUserPage == null)
                    goToUserPage = new RelayCommand<Object>(x =>
                    {
                        Mediator.NotifyColleagues("ChangeView", ViewModelTypes.UserPageViewModel);

                    });
                return goToUserPage;
            }
        }

        public ICommand GoToHomePage
        {
            get
            {
                if (goToHomePage == null)
                    goToHomePage = new RelayCommand<Object>(x =>
                    {
                        Mediator.NotifyColleagues("ChangeView", ViewModelTypes.HomePageViewModel);

                    });
                return goToHomePage;
            }
        }
    }
}
