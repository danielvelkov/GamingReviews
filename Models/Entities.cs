using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Models
{
    public class Entities
    {
        public Entities()
        {
            Target_Comment = new HashSet<Comments>();
            Votes = new HashSet<Votes>();
        }

        // unique and autoincremented
        [Key,Index(IsUnique =true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Entity_Id { get; set; }

        public virtual Articles Articles { get; set; }

        public virtual Reviews Reviews { get; set; }

        public virtual Games Games { get; set; }

        // EXPLENATION:https://www.entityframeworktutorial.net/code-first/inverseproperty-dataannotations-attribute-in-code-first.aspx

        [InverseProperty("Entity")]
        public virtual Comments Comment { get; set; }

        [InverseProperty("TargetEntity")]
        public virtual HashSet<Comments> Target_Comment { get; set; }

        public virtual HashSet<Votes> Votes { get; set; }
    }
}
