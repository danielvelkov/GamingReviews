using GamingReviews.Helper;
using GamingReviews.Interfaces;
using GamingReviews.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace GamingReviews.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        static Users currentUser;
        static Articles selectedArticle;
        static Games selectedGame;
        static Reviews selectedReview;
        

        //TODO: could be optimised

        public Users GetCurrentUser()
        {
            return currentUser;
        }

        public void SetCurrentUser(Users value)
        {
            currentUser = value;
        }

        public Articles GetSelectedArticle()
        {
            return selectedArticle;
        }

        public void SetSelectedArticle(Articles value)
        {
            selectedArticle = value;
        }
        
        public Games GetCurrentGame()
        {
            return selectedGame;
        }

        public void SetSelectedGame(Games Game)
        {
            selectedGame = Game;
        }

        public Reviews GetCurrentReview()
        {
            return selectedReview;
        }

        public void SetSelectedReview(Reviews Review)
        {
            selectedReview = Review;

        }


        protected ICommand goToUserProfile;
        protected ICommand goToHomePage;
        protected ICommand goToGamePage;

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

        public ICommand GoToUserProfile
        {
            get
            {
                if (goToUserProfile == null)
                    goToUserProfile = new RelayCommand<Object>(x =>
                    {
                        Mediator.NotifyColleagues("ChangeView", ViewModelTypes.UserPageViewModel);

                    });
                return goToUserProfile;
            }
        }

        public ICommand GoToGamePage
        {
            get
            {
                if (goToGamePage == null)
                    goToGamePage = new RelayCommand<Object>(x =>
                    {
                        Mediator.NotifyColleagues("ChangeView",
                            ViewModelTypes.GamePageViewModel);
                    });
                return goToGamePage;
            }
        }

        #region propertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
