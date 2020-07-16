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
        

        //TODO: could be optimised, maybe add them to the mediator where you need them
        // so not everyone (and by everyone i mean all the viewmodel classes) has the method

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
        

        #region propertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
