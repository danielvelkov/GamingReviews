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
        public GameNewsLetterContext ArticlesContext
        {
            get { return base.Context as GameNewsLetterContext; }
        }

        // implement the methods from the used interface

        public List<Articles> GetLatestArticles()
        {
           return ArticlesContext.Articles.OrderBy(a => a.Date).Take(1).ToList();
        }
        
    }
}
