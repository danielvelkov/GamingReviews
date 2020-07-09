using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.ViewModels.Handling_Entity_Display
{
    public class ReviewViewModel:BaseViewModel
    {
        public ReviewViewModel()
        {
        }

        #region variables

        Reviews review;

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
                    NotifyPropertyChanged("Review");
                }
            }
        }

        #endregion

        #region commands

        #endregion

    }
}
