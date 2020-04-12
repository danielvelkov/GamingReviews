using GamingReviews.Core.IRepositories;
using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Persistance.Repositories
{
    class CommentsRepository:Repository<Comments>, ICommentsRepository
    {
        public CommentsRepository(DBContext context) : base(context) { }

        public DBContext PlutoContext
        {
            get { return Context as DBContext; }
        }

        // implement the methods from the used interface
    }
}
