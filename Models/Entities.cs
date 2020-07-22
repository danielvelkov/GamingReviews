using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Target_Comment = new ObservableCollection<Comments>();
            Votes = new ObservableCollection<Votes>();
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
        public virtual ObservableCollection<Comments> Target_Comment { get; set; }

        public virtual ObservableCollection<Votes> Votes { get; set; }
    }
}
