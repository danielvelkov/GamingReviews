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
        public CommentsRepository(GameNewsLetterContext context) : base(context) { }

        public GameNewsLetterContext PlutoContext
        {
            get { return Context as GameNewsLetterContext; }
        }

        // implement the methods from the used interface
    }
}
