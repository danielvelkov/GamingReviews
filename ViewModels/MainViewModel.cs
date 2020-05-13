﻿using GamingReviews.Helper;
using System;
using System.Windows.Input;
using GamingReviews.Views;
using GamingReviews.Interfaces;
using System.Collections.Generic;

namespace GamingReviews.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand updateViewCommand;
        private BaseViewModel _currentContent;
        private List<BaseViewModel> history;
        
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
            CurrentContent = ViewModelsFactory.ViewModelType((ViewModelTypes)Enum.Parse(typeof(ViewModelTypes),ViewModel.ToString()));
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
