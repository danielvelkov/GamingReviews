using GamingReviews.Helper;
using System;
using System.Windows.Input;
using GamingReviews.Views;
using GamingReviews.Interfaces;

namespace GamingReviews.ViewModels
{
    class MainViewModel:BaseViewModel
    {
        private ICommand changeContentCommand;
        private BaseViewModel currentContent;

        public ICommand ChangeContentCommand
        {
            get
            {
                if (changeContentCommand == null)
                    changeContentCommand = new RelayCommand<Object>(ChangeContent);

                return changeContentCommand;
            }
        }

        public void ChangeContent(object param)
        {
            var currentType = (ViewModelTypes)param;

            CurrentContent = ViewModelsFactory.ViewModelType(currentType);
        }

        public BaseViewModel CurrentContent
        {
            get
            {
                if (currentContent == null)
                    ChangeContent(ViewModelTypes.LoginPageViewModel);
                return currentContent;
            }
            set
            {
                if (currentContent == value)
                    return;

                currentContent = value;
                
                NotifyPropertyChanged(nameof(CurrentContent));
            }
        }
    }
}
