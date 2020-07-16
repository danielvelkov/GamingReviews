using GamingReviews.Helper;
using GamingReviews.Interfaces;
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
        ObservableCollection<IEntity> articlesReviews=new ObservableCollection<IEntity>();

        #region parameters
        public ObservableCollection<IEntity> ArticlesReviews
        {
            get
            {
                if (articlesReviews.Count==0)
                {
                    using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                    {
                        foreach (Articles entity in unitOfWork.Articles.GetLatestArticles())
                            articlesReviews.Add(entity);
                        foreach (Reviews entity in unitOfWork.Reviews.GetLatestReviews())
                            articlesReviews.Add(entity);
                        
                    }
                    
                }
                
                return articlesReviews;
            }
            set
            {
                if (articlesReviews != value)
                {
                    articlesReviews = value;
                    NotifyPropertyChanged("ArticlesReviews");
                }
            }
        }
        #endregion

        #region commands

        ICommand readArticle;
        ICommand readReview;

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
                    });
                return readArticle;
            }
        }
        public ICommand ReadReview
        {
            get
            {
                if (readReview == null)
                    readReview = new RelayCommand<Reviews>(x =>
                    {
                        this.SetSelectedReview(x);
                        Mediator.NotifyColleagues("ChangeView",
                            ViewModelTypes.ReviewViewModel);
                    });
                return readReview;
            }
        }


        #endregion
    }
}
