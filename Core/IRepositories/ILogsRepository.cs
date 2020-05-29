using GamingReviews.Interfaces;
using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Core.IRepositories
{
    public interface ILogsRepository: IRepository<Logs>
    {
        List<Logs> GetUserLogs(int user_id);
    }
}
