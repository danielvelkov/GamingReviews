using GamingReviews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GamingReviews.Helper
{
    public class UpdateViewCommand : ICommand
    {
        private BaseViewModel mainViewModel;

        public UpdateViewCommand(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public BaseViewModel MainViewModel
        {
            get
            {
                return mainViewModel;
            }
            set
            {
                mainViewModel = value;
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "HomePageViewModel")
            {
                mainViewModel = ViewModelsFactory.ViewModelType(ViewModelTypes.HomePageViewModel);
            }
            if (parameter.ToString() == "LoginPageViewModel")
            {
                mainViewModel = ViewModelsFactory.ViewModelType(ViewModelTypes.LoginPageViewModel);
            }
        }
    }
}
