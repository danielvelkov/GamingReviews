using GamingReviews.Core.IRepositories;
using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Persistance.Repositories
{
    class GamesRepository: Repository<Games>, IGamesRepository
    {

        public GamesRepository(GameNewsLetterContext context) : base(context) { }

        public GameNewsLetterContext PlutoContext
        {
            get { return Context as GameNewsLetterContext; }
        }

        public bool Any()
        {
            Games game = PlutoContext.Games.SingleOrDefault(g => g.Id > 0);
            if (game == null)
            {
                return false;
            }
            else return true;
        }

        // implement the methods from the used interface

    }
}
