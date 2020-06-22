using GamingReviews.Core.IRepositories;
using GamingReviews.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Core
{
    interface IUnitOfWork:IDisposable
    {
        
        int Complete();
    }
}
