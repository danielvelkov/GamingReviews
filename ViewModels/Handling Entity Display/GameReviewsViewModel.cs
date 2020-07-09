using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace GamingReviews.ViewModels.Handling_Entity_Display
{
    class GameReviewsViewModel:BaseViewModel
    {
        #region variables
        Games game;
        List<Reviews> reviews;
        #endregion

        #region parameters
        public Games Game
        {
            get
            {
                return game;
            }
            set
            {
                if (game != value) 
                game = value;
            }
        }

        public List<Reviews> Reviews
        {
            get
            {
                return reviews;
            }
            set
            {
                reviews = value;
            }
        }
        #endregion

        public GameReviewsViewModel()
        {
            Game = this.GetCurrentGame();
            using(var unitOfWork= new UnitOfWork(new GameNewsLetterContext()))
            {
                reviews = unitOfWork.Reviews.Find(x => x.Game_id == Game.Entity_id) as List<Reviews>;
            }
        }

        #region commands
        ICommand readReview;

        public ICommand ReadReview
        {
            get
            {
                if (readReview == null)
                    readReview = new RelayCommand<Object>(x =>
                    {
                        this.SetSelectedReview((Reviews)x);
                        Mediator.NotifyColleagues("ChangeView", ViewModelTypes.ReviewViewModel);

                    });
                return readReview;
            }
        }

        #endregion

    }
}
