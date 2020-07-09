using GamingReviews.Core.IRepositories;
using GamingReviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Persistance.Repositories
{
    class VotesRepository: Repository<Votes>, IVotesRepository
    {
        public VotesRepository(GameNewsLetterContext context) : base(context)
        {
        }
        public GameNewsLetterContext VotesContext
        {
            get { return Context as GameNewsLetterContext; }
        }

        public Reaction GetReaction(int Entity_id,int User_id)
        {

            Votes vote= VotesContext.Votes.First(x=>(x.Entity_id==Entity_id && x.User_id==User_id));
            return vote.Reaction;
        }

        public int GetVotes(int Entity_Id)
        {
            //IQueryable<Votes> allVotes= VotesContext.Votes.Where(x => (x.Entity_id == Entity_Id));
            List<Votes> allvotes=new List<Votes>();
            Entities entity = VotesContext.Entities.FirstOrDefault(x => (x.Entity_Id == Entity_Id));
            foreach (var vote in entity.Votes)
                allvotes.Add(vote);
            int result = 0;
            foreach (Votes vote in allvotes)
                if (vote.Reaction == Reaction.Liked)
                    result++;
                else result--;
            return result;
        }

        public bool HasVoted(int Entity_Id, int User_Id)
        {
            if (VotesContext.Votes.Any(x => (x.Entity_id == Entity_Id && x.User_id == User_Id)))
                return true;
            else return false;
        }

        // ehh idk could be smarter 
        public void ChangeVote(int Entity_Id, int User_Id,Reaction reac)
        {
            Votes vote = VotesContext.Votes.FirstOrDefault(x => (x.Entity_id == Entity_Id && x.User_id==User_Id));
            if (vote != null)
            {
                vote.Reaction = reac;
                VotesContext.SaveChanges();
            }
        }

        // implement the methods from the used interface


    }
}
