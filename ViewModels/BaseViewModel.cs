using GamingReviews.Interfaces;
using GamingReviews.Models;
using System;
using System.ComponentModel;


namespace GamingReviews.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        static Users currentUser;
        static Articles selectedArticle;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }
}
