using GamingReviews.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Core
{
    interface IUnitOfWork:IDisposable
    {

        IReviewsRepository Reviews { get; }
        IUserRepository Users { get; }
        IArticlesRepository Articles { get; }
        int Complete();
    }
}
