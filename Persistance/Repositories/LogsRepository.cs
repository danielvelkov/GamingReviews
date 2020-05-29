using GamingReviews.Core.IRepositories;
using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Persistance.Repositories
{
    class LogsRepository:Repository<Logs>,ILogsRepository
    {
        public LogsRepository(GameNewsLetterContext context) : base(context)
        {
        }
        public GameNewsLetterContext PlutoContext
        {
            get { return Context as GameNewsLetterContext; }
        }

        public List<Logs> GetUserLogs(int user_id)
        {
            using (var unit=new UnitOfWork(PlutoContext))
            {
                return unit.Logs.GetAll().ToList<Logs>();
            }
        }

        // implement the methods from the used interface

    }
}
