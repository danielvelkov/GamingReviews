using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GamingReviews.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        List<Articles> articles;

        public List<Articles> Articles
        {
            get
            {
                if (articles == null)
                {
                    using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                    {
                        
                        articles = unitOfWork.Articles.GetLatestArticles();
                        if (articles.Count == 0)
                        {
                            
                            // add a dummy article
                           
                        }
                        else
                        NotifyPropertyChanged("Articles");
                        unitOfWork.Complete();
                        
                    }
                    
                }
                
                return articles;
            }
        }

        ICommand goToUserProfile;
        ICommand readArticle;
        ICommand goToGamePage;

        public ICommand GoToUserProfile
        {
            get
            {
                if (goToUserProfile == null)
                    goToUserProfile = new RelayCommand<Object>
                        (x=> 
                        {
                            Mediator.NotifyColleagues("ChangeView",
                                ViewModelTypes.UserPageViewModel);
                        }, () => { return true; });
                return goToUserProfile;
            }
        }

        public ICommand ReadArticle
        {
            get
            {
                if (readArticle == null)
                    readArticle = new RelayCommand<Articles>(x => 
                    {
                        this.SetSelectedArticle(x);
                        Mediator.NotifyColleagues("ChangeView",
                            ViewModelTypes.ArticleViewModel);
                    },()=> { return true; });
                return readArticle;
            }
        }

        public ICommand GoToGamePage
        {
            get
            {
                if (goToGamePage == null)
                    goToGamePage = new RelayCommand<Object>(x =>
                    {
                        Mediator.NotifyColleagues("ChangeView",
                            ViewModelTypes.GamePageViewModel);
                    }, () => { return true; });
                return goToGamePage;
            }
        }

        
    }
}
