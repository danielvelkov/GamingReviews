using GamingReviews.Helper;
using GamingReviews.Interfaces;
using GamingReviews.Models;
using GamingReviews.Persistance;
using GamingReviews.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GamingReviews.ViewModels
{
    class RegisterPageViewModel:BaseViewModel
    {
        #region fields

        string userName;
        string password;
        string confirmPassword;
        string email;
        string errorMsg;

        #endregion

        #region parameters

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

        public string ErrorMsg
        {
            get
            {
                if (errorMsg == null)
                {
                    errorMsg = "Password empty";
                }
                return errorMsg;
            }
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    
                    NotifyPropertyChanged("ErrorMsg");
                }
            }
        }


        #endregion

        #region commands

        ICommand registerUser;

        public ICommand RegisterUser
        {
            get
            {
                if (registerUser == null)
                    registerUser = new RelayCommand<Object>(RegisterUserToDB);
                return registerUser;
            }
        }

        #endregion

        public void RegisterUserToDB()
        {
            
            // first check if the username exists 
            // then check if the password match

            // register user service
            if (Password == ConfirmPassword) 
            {
                Users newUser = new Users(UserName, "USER", Password, new byte[0],Email);
                using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                {
                    if (!unitOfWork.Users.DoesUserExist(UserName))
                    {
                        // then check if its an valid email
                        // then check if image is null  (not sure)
                        unitOfWork.Users.Add(newUser);
                    }
                    else
                    {
                        // set the error message to "user already exists"
                        
                    }
                    unitOfWork.Complete();
                }
            }
            else
            {
                // set the error message to "passwords dont match"
                ErrorMsg = "Passwords dont match";
            }
            
            //App.Current.MainWindow.Content = ViewModelsFactory.ViewModelType(ViewModelTypes.LoginPageViewModel);
        }
    }
}
