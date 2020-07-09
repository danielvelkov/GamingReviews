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
        Reaction GetReaction(int Entity_id, int User_id);
        void ChangeVote(int Entity_Id,int User_Id,Reaction react);
        int GetVotes(int Entity_Id);
        bool HasVoted(int Entity_Id, int User_Id);
    }
}
