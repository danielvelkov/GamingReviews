using GamingReviews.Interfaces;
using GamingReviews.Models;
using System;
using System.ComponentModel;


namespace GamingReviews.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IView View { get; set; }
        

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
            if (PropertyChanged != null)
            {
                PropertyChanged(null, new PropertyChangedEventArgs(nameof(propertyName)));
            }
        }
        
    }
}
