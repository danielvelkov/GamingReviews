﻿using GamingReviews.Core.Repositories;
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

        public GameNewsLetterContext UsersContext
        {
            get { return Context as GameNewsLetterContext; }
        }

        // implement the methods from the used interface
        public bool DoesUserExist(string Username)
        {
            Users user =UsersContext.Users.SingleOrDefault(a => a.UserName == Username);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public bool DoesEmailExist(string Email)
        {
            Users user = UsersContext.Users.SingleOrDefault(a => a.Email == Email);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public void UpdatePassword(int id,string newPassword)
        {
            Users user = UsersContext.Users.SingleOrDefault(a => a.Id == id);
            user.Password = newPassword;
            UsersContext.SaveChanges();
        }

        public void UpdateImage(int id, byte[] image)
        {
            Users user = UsersContext.Users.SingleOrDefault(a => a.Id == id);
            user.Image = image.ToArray();
            UsersContext.SaveChanges();
        }
    }
}
