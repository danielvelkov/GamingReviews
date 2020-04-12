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
        public UserRepository(DBContext context) : base(context) { }

        public DBContext PlutoContext
        {
            get { return Context as DBContext; }
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
