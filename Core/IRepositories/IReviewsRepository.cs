using GamingReviews.Interfaces;
using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Core.Repositories
{
    public interface IReviewsRepository:IRepository<Reviews>
    {
        // methods concerning reviews
        List<Reviews> GetLatestReviews();
        List<Reviews> GetReviewsByGameId(int Entity_id);
    }
}
