using GamingReviews.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Models
{
    public class ArticlesRepository : Repository<Articles>, IArticlesRepository 
    {
        public ArticlesRepository(GameNewsLetterContext context): base(context)
        {
        }
        public GameNewsLetterContext PlutoContext
        {
            get { return Context as GameNewsLetterContext; }
        }

        // implement the methods from the used interface


    }
}
