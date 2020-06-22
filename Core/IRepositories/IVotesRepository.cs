using GamingReviews.Interfaces;
using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Core.IRepositories
{
    public interface IVotesRepository:IRepository<Votes>
    {
        void ChangeVote(int Entity_Id,int User_Id);
        int GetVotes(int Entity_Id);
        bool HasVoted(int Entity_Id, int User_Id);
    }
}
