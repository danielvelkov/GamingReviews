using GamingReviews.Core.IRepositories;
using GamingReviews.Core.Repositories;
using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Persistance.Repositories
{
    class EntitiesRepository : Repository<Entities>, IEntityRepository
    {
        public EntitiesRepository(GameNewsLetterContext context) : base(context)

        {

        }

        public GameNewsLetterContext PlutoContext
        {
            get { return Context as GameNewsLetterContext; }
        }

        // implement the methods from the used interface

    }  
}
