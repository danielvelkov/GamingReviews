using GamingReviews.Interfaces;
using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Core.Repositories
{
    public interface IUserRepository:IRepository<Users>
    {
        // methods concerning 
        bool DoesUserExist(string Username);
        bool DoesEmailExist(string Email);
        void UpdatePassword(int id, string Password);
        void UpdateImage(int id, byte[] Image);
    }
}
