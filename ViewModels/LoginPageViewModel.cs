using GamingReviews.Helper;
using GamingReviews.Interfaces;
using GamingReviews.Models;
using GamingReviews.Persistance;
using GamingReviews.Views;
using System;
using System.Windows.Input;

namespace GamingReviews.ViewModels
{
    class LoginPageViewModel:BaseViewModel
    {

        MainViewModel  _model;
        private string userName;
        //private User user;

        ICommand loginCommand;
        ICommand registerCommand;

        public LoginPageViewModel(MainViewModel vm)
        {
            _model = vm;
            
        }

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

        // you get the password at the password changed event. SEE  the codebehind-> LoginPageView.xaml.cs 
        public string Password { private get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand =
                  new RelayCommand<Object>(x =>
                  {
                      using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                      {
                          Users user = unitOfWork.Users.SingleOrDefault(u => u.UserName == UserName && u.password == Password);
                          if (user == null)
                          {
                              //err message that the user doesnt exist
                              return;
                          }
                          // sets the current user
                          this.SetCurrentUser(user);
                      }
                      Mediator.NotifyColleagues("ChangeView", ViewModelTypes.HomePageViewModel);
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

        private void LoginUser()
        {
            // call service to get user
            
           using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
           {
                Users user=unitOfWork.Users.SingleOrDefault(u => u.UserName == UserName && u.password == Password);
                if (user == null)
                {
                    //err message that the user doesnt exist
                    return;
                }
                // sets the current user
                this.SetCurrentUser(user);
           }
            // change to login screen and viewmodel
             App.Current.MainWindow.Content = ViewModelsFactory.ViewModelType(ViewModelTypes.HomePageViewModel);
            
        }

        
    }
}
