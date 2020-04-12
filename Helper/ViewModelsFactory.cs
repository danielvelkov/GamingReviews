using GamingReviews.Interfaces;
using GamingReviews.ViewModels;
using GamingReviews.Views;
using System;

namespace GamingReviews.Helper
{

    public static class ViewModelsFactory
    {
        public static BaseViewModel ViewModelType(ViewModelTypes currentType)
        {
            IView currentView;
            BaseViewModel currentViewModel;
            switch (currentType)
            {
                case ViewModelTypes.LoginPageViewModel:
                    {
                        currentView = new LoginPageView();
                        currentViewModel = new LoginPageViewModel();
                        currentView.DataContext = currentViewModel;
                        currentViewModel.View = currentView;

                        return currentViewModel;
                    }
                case ViewModelTypes.HomePageViewModel:
                    {
                        currentView = new HomePageView();
                        currentViewModel = new HomePageViewModel();
                        currentView.DataContext = currentViewModel;
                        currentViewModel.View = currentView;

                        return currentViewModel;
                    }
                case ViewModelTypes.GamePageViewModel:
                    {
                        currentView = new GamePageView();
                        currentViewModel = new GamePageViewModel();
                        currentView.DataContext = currentViewModel;
                        currentViewModel.View = currentView;

                        return currentViewModel;
                    }
                case ViewModelTypes.UserPageViewModel:
                    {
                        currentView = new UserPageView();
                        currentViewModel = new Testuser();
                        currentView.DataContext = currentViewModel;
                        currentViewModel.View = currentView;

                        return currentViewModel;
                    }
                case ViewModelTypes.RegisterPageViewModel:
                    {
                        currentView = new RegisterPageView();
                        currentViewModel = new RegisterPageViewModel();
                        currentView.DataContext = currentViewModel;
                        currentViewModel.View = currentView;

                        return currentViewModel;
                    }
                case ViewModelTypes.ArticleViewModel:
                    {
                        currentView = new ArticleView();
                        currentViewModel = new ArticleViewModel();
                        currentView.DataContext = currentViewModel;
                        currentViewModel.View = currentView;

                        return currentViewModel;
                    }
                default:
                    return new BaseViewModel();
            }
        }
    }
}
