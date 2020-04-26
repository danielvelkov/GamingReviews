using GamingReviews.Helper;
using System;
using System.Windows.Input;
using GamingReviews.Views;
using GamingReviews.Interfaces;

namespace GamingReviews.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand updateViewCommand;
        private BaseViewModel _currentContent;

        public MainViewModel()
        {
            Mediator.Register("ChangeView", ChangeContent);

            
        }

        public ICommand UpdateViewCommand {
            get
            {
                return updateViewCommand ?? (updateViewCommand = new RelayCommand<Object>(x =>
                   {
                       Mediator.NotifyColleagues("ChangeView", x);
                   }));
            }
        }

        void ChangeContent(object ViewModel )
        {
            CurrentContent = ViewModelsFactory.ViewModelType((ViewModelTypes)ViewModel);
        }

        public BaseViewModel CurrentContent
        {
            get
            {
                if (_currentContent == null)
                    _currentContent = new LoginPageViewModel(this);
                return _currentContent;
            }
            set
            {
                if (_currentContent == value)
                    return;

                _currentContent = value;
                NotifyPropertyChanged("CurrentContent");
            }
        }
        
    }
}
