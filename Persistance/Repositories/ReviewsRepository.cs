using GamingReviews.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Models
{
    class ReviewsRepository:Repository<Reviews>, IReviewsRepository
    {
        public ReviewsRepository(GameNewsLetterContext context) : base(context) { }

        public GameNewsLetterContext ReviewsContext
        {
            get { return Context as GameNewsLetterContext; }
        }

        public List<Reviews> GetLatestReviews()
        {
            return ReviewsContext.Reviews.OrderBy(a => a.Date).ToList();
        }

        public List<Reviews> GetReviewsByGameId(int Entity_id)
        {
            return ReviewsContext.Reviews.Where(x => x.Game_id == Entity_id).ToList();
        }

        // implement the methods from the used interface

    }
}
