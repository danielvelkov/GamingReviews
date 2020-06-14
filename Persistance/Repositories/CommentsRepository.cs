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

        public GameNewsLetterContext CommentContext
        {
            get { return Context as GameNewsLetterContext; }
        }
        
        public List<Comments> FindAllComments(int Entity_id)
        {
            return CommentContext.Comments.Where(x => x.TargetEntity_Id == Entity_id).ToList();
        }

        // implement the methods from the used interface
    }
}
