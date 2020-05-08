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
                        currentViewModel = new LoginPageViewModel(null);
                        currentView.DataContext = currentViewModel;
                       

                        return currentViewModel;
                    }
                case ViewModelTypes.HomePageViewModel:
                    {
                        currentView = new HomePageView();
                        currentViewModel = new HomePageViewModel();
                        currentView.DataContext = currentViewModel;
                        

                        return currentViewModel;
                    }
                case ViewModelTypes.GamePageViewModel:
                    {
                        currentView = new GamePageView();
                        currentViewModel = new GamePageViewModel();
                        currentView.DataContext = currentViewModel;
                        

                        return currentViewModel;
                    }
                case ViewModelTypes.UserPageViewModel:
                    {
                        currentView = new UserPageView();
                        currentViewModel = new UserPageViewModel();
                        currentView.DataContext = currentViewModel;
                        

                        return currentViewModel;
                    }
                case ViewModelTypes.RegisterPageViewModel:
                    {
                        currentView = new RegisterPageView();
                        currentViewModel = new RegisterPageViewModel();
                        currentView.DataContext = currentViewModel;
                       

                        return  (RegisterPageViewModel)currentView.DataContext;
                    }
                case ViewModelTypes.ArticleViewModel:
                    {
                        currentView = new ArticleView();
                        currentViewModel = new ArticleViewModel();
                        currentView.DataContext = currentViewModel;
                        

                        return currentViewModel;
                    }
                default:
                    return new BaseViewModel();
            }
        }
    }
}
