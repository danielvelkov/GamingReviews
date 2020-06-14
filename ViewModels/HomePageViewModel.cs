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

        #region parameters
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
        #endregion

        #region commands

        ICommand readArticle;

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

        #endregion
    }
}
