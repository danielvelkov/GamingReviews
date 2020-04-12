namespace GamingReviews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reviews
    {
        public int Id { get; set; }

        public int User_id { get; set; }

        public Reviews(int user_id, string name, string content, int game_id)
        {
            this.User_id = user_id;
            this.name = name;
            this.Content = content;
            this.Game_id = game_id;
        }


        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        public string Content { get; set; }

        public int Game_id { get; set; }

        public virtual Games Games { get; set; }

        public virtual Users Users { get; set; }
    }
}
