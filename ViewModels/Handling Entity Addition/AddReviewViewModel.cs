using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GamingReviews.ViewModels.Handling_Entity_Addition
{
    class AddReviewViewModel:BaseViewModel
    {

        public AddReviewViewModel()
        {
            Review = new Reviews();
        }

        #region fields
        Reviews review;
        List<Games> games;
        Games selectedGame;
        int game_id;
        #endregion

        #region parameters
        public Reviews Review
        {
            get
            {
                return review;
            }
            set
            {
                if (review != value)
                {
                    review = value;

                }
            }
        }
        public List<Games> GamesList
        {
            get
            {
                if (games == null)
                {
                    using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                    {
                        games = new List<Games>();
                        games.AddRange(unitOfWork.Games.GetAll());
                    }
                }

                return games;
            }
        }
        public Games SelectedGame
        {
            get
            {
                return selectedGame;
            }
            set
            {
                selectedGame = value;
            }
        }
        public int Game_id
        {
            get
            {
                game_id = SelectedGame.Entity_id;
                return game_id;
            }
        }
        #endregion

        #region commands
        ICommand addReview;
        
        public ICommand AddReview
        {
            get
            {
                if (addReview == null)
                {
                    addReview = new RelayCommand<Object>(x =>
                    {
                        using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                        {
                            var Entity = new Entities();
                            unitOfWork.Entities.Add(Entity);
                            unitOfWork.Complete();
                            
                            Review.User_id = GetCurrentUser().Id;
                            Review.Game_id = Game_id;
                            Review.Date = DateTime.Now;

                            unitOfWork.Reviews.Add(Review);

                            Review.Entity = Entity;
                            unitOfWork.Complete();

                            MessageBox.Show("Review added succesfully", "Review added", MessageBoxButton.OK);

                            SetSelectedReview(Review);
                            Mediator.NotifyColleagues("ChangeView", ViewModelTypes.ReviewViewModel);

                        }
                    }, () =>
                    {
                        //{
                        //    if(String.IsNullOrEmpty(Review.Header))
                        //    return false;
                        //    else if (String.IsNullOrEmpty(Review.Name))
                        //        return false;
                        //    else if (String.IsNullOrEmpty(Review.Content))
                        //        return false;

                        return true;
                    });
                }
                return addReview;
            }
        }
        #endregion
    }
}

