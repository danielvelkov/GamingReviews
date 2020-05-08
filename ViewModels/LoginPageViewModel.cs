using GamingReviews.Helper;
using GamingReviews.Interfaces;
using GamingReviews.Models;
using GamingReviews.Persistance;
using GamingReviews.Views;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace GamingReviews.ViewModels
{
    class LoginPageViewModel:BaseViewModel
    {
        #region fields
        string userName;
        string errorMsg;
        ICommand loginCommand;
        ICommand registerCommand;
        #endregion

        public LoginPageViewModel(MainViewModel vm)
        {
            
            
        }

        #region Parameters
        public string UserName
        {
            get { return userName; }
            set
            {
                // probably needs some validation here
                if (userName != value)
                {
                    userName = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }

        public string ErrorMsg
        {
            get { return errorMsg; }
            set
            {
                // probably needs some validation here
                if (errorMsg != value)
                {
                    errorMsg = value;
                    NotifyPropertyChanged("ErrorMsg");
                }
            }
        }

        // you get the password at the password changed event. SEE  the codebehind-> LoginPageView.xaml.cs 
        public string Password { private get; set; }
        #endregion

        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand =
                  new RelayCommand<Object>(x =>
                  {
                      using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                      {
                          Users user = unitOfWork.Users.SingleOrDefault(u => u.UserName == UserName);
                          if (user == null)
                          {
                              //err message that the user doesnt exist
                              ErrorMsg = "User does not exist";
                              return;
                          }
                          //cuz it returns password from sql with whitespaces in the end
                          user.password=Regex.Replace(user.password, @"\s+", "");
                          if (user.password==Password)
                          {
                              // sets the current user
                              this.SetCurrentUser(user);
                              // change to home screen
                              Mediator.NotifyColleagues("ChangeView", ViewModelTypes.HomePageViewModel);
                          }
                          ErrorMsg = "password doesnt match";
                          return;
                         
                      }
                     
                  }));
               
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return registerCommand ?? (registerCommand =
                 new RelayCommand<Object>(x =>
                 {
                     Mediator.NotifyColleagues("ChangeView", ViewModelTypes.RegisterPageViewModel);
                 }));
            }
        }

        
    }
}
