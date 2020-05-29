using GamingReviews.ViewModels;
using System;
using System.Windows.Input;

namespace GamingReviews.Helper
{
    class RelayCommand<T>:ICommand where T:class
    {
        public event EventHandler CanExecuteChanged;


        private Action<T> method;
        private Func<bool> methodNoParams;

        public RelayCommand(Action<T> methodToExecute,Func<bool> func)
        {
            
            this.method = methodToExecute;
            this.methodNoParams = func;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
        
        public bool CanExecute(object parameter)
        {
            if (methodNoParams != null)
                return methodNoParams();
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
