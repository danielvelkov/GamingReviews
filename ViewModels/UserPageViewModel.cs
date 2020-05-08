using GamingReviews.Helper;
using GamingReviews.Models;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GamingReviews.ViewModels
{
    class UserPageViewModel : BaseViewModel
    {
        #region fields

        string userName;
        string password;
        string confirmPassword;
        string email;
        string type;
        BitmapImage profilePic; 

        #endregion

        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    NotifyPropertyChanged("ConfirmPassword");
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        public BitmapImage ProfilePic
        {
            get { return profilePic; }
            set
            {
                if (profilePic != value)
                {
                    profilePic = value;
                    NotifyPropertyChanged("ProfilePic");
                }
            }
        }

        ICommand saveChanges;

        public UserPageViewModel()
        {
            Users CurrentUser = base.GetCurrentUser();
            UserName = CurrentUser.UserName;
            Email = CurrentUser.Email;
            UserName = CurrentUser.UserName;
            Type = CurrentUser.UserType;

            ByteArrayToBitmapImageConverter converter = new ByteArrayToBitmapImageConverter();
            ProfilePic = (BitmapImage)converter.Convert(CurrentUser.image, null, null, null);
        }

        public ICommand SaveChanges
        {
            get
            {
                if (saveChanges == null)
                    saveChanges = new RelayCommand<Object>(SaveChangesToDB);
                return saveChanges;
            }
        }

        public void SaveChangesToDB()
        {
            // first check if the username exists 
            // then check if the password match
            if (Password == ConfirmPassword) //do something
                Password=Password;
            // then check if its an valid email

            // find where the current user is
            // we will probably need to pass it to this viewmodel before hand
            // when we have the current user update his values with the new ones
            Users t=this.GetCurrentUser();
            t.UserName = UserName;
            App.Current.MainWindow.Content = ViewModelsFactory.ViewModelType(ViewModelTypes.HomePageViewModel);
        }

    }
}
