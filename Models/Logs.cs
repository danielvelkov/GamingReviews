using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Models
{
    public partial class Logs
    {

        public Logs(int user_id, string activity)
        {
            User_id = user_id;
            Activity = activity;
            Date = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Activity { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public int User_id { get; set; }

        public Users User { get; set; }

    }
}
