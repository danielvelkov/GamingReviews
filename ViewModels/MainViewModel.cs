using GamingReviews.Helper;
using System;
using System.Windows.Input;
using GamingReviews.Views;
using GamingReviews.Interfaces;
using System.Collections.Generic;

namespace GamingReviews.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region variables

        private ICommand updateViewCommand;
        private ICommand signOut;
        private BaseViewModel _currentContent;
        //private List<BaseViewModel> history;

        #endregion

        #region parameters

        public BaseViewModel CurrentContent
        {
            get
            {
                if (_currentContent == null)
                {
                    _currentContent = new LoginPageViewModel(this);
                }
                return _currentContent;
            }
            set
            {
                if (_currentContent == value)
                    return;

                // enables the menu items after you log in
                if ((_currentContent is LoginPageViewModel) && (value is HomePageViewModel))
                {
                    (UpdateViewCommand as RelayCommand<Object>).RaiseCanExecuteChanged();
                }
                _currentContent = value;
                NotifyPropertyChanged("CurrentContent");
            }
        }

        #endregion

        public MainViewModel()
        {
            Mediator.Register("ChangeView", ChangeContent);
        }

        #region commands

        public ICommand UpdateViewCommand {
            get
            {
                return updateViewCommand ?? (updateViewCommand = new RelayCommand<Object>(x =>
                   {
                       Mediator.NotifyColleagues("ChangeView", x);
                   }, () => { if (this.GetCurrentUser() != null) return true; else return false; }
                ));
            }
        }

        public ICommand SignOut
        {
            get
            {
                return signOut ?? (signOut = new RelayCommand<Object>(x =>
                {
                    Mediator.NotifyColleagues("ChangeView", x);
                    this.SetCurrentUser(null);

                    // disables menu
                    (UpdateViewCommand as RelayCommand<Object>).RaiseCanExecuteChanged();
                }, () => { return true; }
                ));
            }
        }

        #endregion

        #region methods

        void ChangeContent(object ViewModel )
        {
            // this is ugly
            CurrentContent = ViewModelsFactory.ViewModelType((ViewModelTypes)Enum.Parse(typeof(ViewModelTypes),ViewModel.ToString()));
        }

        #endregion
    }
}
