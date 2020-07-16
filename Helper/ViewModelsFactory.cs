using GamingReviews.Interfaces;
using GamingReviews.ViewModels;
using GamingReviews.ViewModels.Handling_Entity_Addition;
using GamingReviews.ViewModels.Handling_Entity_Display;
using GamingReviews.Views;
using GamingReviews.Views.Views_for_Adding;
using GamingReviews.Views.Views_for_Displaying;

namespace GamingReviews.Helper
{

    public static class ViewModelsFactory
    {

        public static BaseViewModel ViewModelType(ViewModelTypes currentType)
        {
            IView currentView;
            BaseViewModel currentViewModel;

            //cringe switch
            switch (currentType)
            {
                case ViewModelTypes.LoginPageViewModel:
                    {
                        currentView = new LoginPageView();
                        currentViewModel = new LoginPageViewModel();
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
                case ViewModelTypes.GameReviewsViewModel:
                    {
                        currentView = new GameReviewsView();
                        currentViewModel = new GameReviewsViewModel();
                        currentView.DataContext = currentViewModel;
                        
                        return currentViewModel;
                    }
                case ViewModelTypes.ReviewViewModel:
                    {
                        currentView = new ReviewView();
                        currentViewModel = new ReviewViewModel();
                        currentView.DataContext = currentViewModel;

                        return currentViewModel;
                    }
                case ViewModelTypes.AddArticleViewModel:
                    {
                        currentView = new AddArticleView();
                        currentViewModel = new AddArticleViewModel();
                        currentView.DataContext = currentViewModel;

                        return currentViewModel;
                    }
                case ViewModelTypes.AddGameViewModel:
                    {
                        currentView = new AddGameView();
                        currentViewModel = new AddGameViewModel();
                        currentView.DataContext = currentViewModel;

                        return currentViewModel;
                    }
                case ViewModelTypes.AddReviewViewModel:
                    {
                        currentView = new AddReviewView();
                        currentViewModel = new AddReviewViewModel();
                        currentView.DataContext = currentViewModel;

                        return currentViewModel;
                    }
                default: // should throw exception
                    return new BaseViewModel();
            }
        }
    }
}
