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
        public ArticlesRepository(DBContext context): base(context)
        {
        }
        public DBContext PlutoContext
        {
            get { return Context as DBContext; }
        }

        // implement the methods from the used interface


    }
}
