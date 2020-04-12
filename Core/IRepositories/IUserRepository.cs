﻿using GamingReviews.Interfaces;
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
    }
}
