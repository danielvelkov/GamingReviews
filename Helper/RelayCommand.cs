using GamingReviews.ViewModels;
using System;
using System.Windows.Input;

namespace GamingReviews.Helper
{
    class RelayCommand<T>:ICommand where T:class
    {
        public event EventHandler CanExecuteChanged;


        private Action<T> method;
        private Action methodNoParams;

        public RelayCommand(Action<T> methodToExecute)
        {
            this.method = methodToExecute;
        }

        public RelayCommand(Action methodToExecute)
        {
            this.methodNoParams = methodToExecute;
        }
        

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (method != null)
            {
                this.method.Invoke((T)parameter);
            }
            else
                this.methodNoParams.Invoke();
        }
    }
}
