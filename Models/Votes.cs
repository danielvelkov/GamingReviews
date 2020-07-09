using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Models
{
    public enum Reaction
    {
        Liked,
        Disliked,
        None
    }

    public partial class Votes
    {
        public Votes()
        {

        }

        public Votes(int EntityId, int UserId,Reaction reaction)
        {
            this.Entity_id = EntityId;
            this.User_id = UserId;
            this.Reaction = reaction;
        }

        public Votes( int UserId, Reaction reaction)
        {
            this.User_id = UserId;
            this.Reaction = reaction;
        }


        [Key,ForeignKey("Entity"),Column(Order = 0)]
        public int Entity_id { get; set; }

        [Key,ForeignKey("User"),Column(Order = 1)]
        public int User_id { get; set; }

        public Reaction Reaction { get; set; }

        public virtual Entities Entity { get; set; }

        public virtual Users User { get; set; }

    }
}
