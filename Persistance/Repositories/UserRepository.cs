using GamingReviews.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Models
{
    class UserRepository:Repository<Users>, IUserRepository
    {
        public UserRepository(GameNewsLetterContext context) : base(context) { }

        public GameNewsLetterContext PlutoContext
        {
            get { return Context as GameNewsLetterContext; }
        }

        // implement the methods from the used interface
        public bool DoesUserExist(string Username)
        {
            Users user =PlutoContext.Users.SingleOrDefault(a => a.UserName == Username);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        
    }
}
