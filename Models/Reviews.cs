namespace GamingReviews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reviews
    {

        public Reviews(int EntityId,int user_id, string name, 
            string content, int game_id)
        {
            Entity_id = EntityId;
            this.User_id = user_id;
            this.Name = name;
            this.Content = content;
            this.Game_id = game_id;
            this.Date = DateTime.Now;
        }

        [Required]
        [Key, ForeignKey("Entity")]
        public int Entity_id { get; set; }

        [ForeignKey("User")]
        public int User_id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("Game")]
        public int Game_id { get; set; }

        public DateTime Date { get; set; }

        public Entities Entity { get; set; }

        public virtual Users User { get; set; }

        public virtual Games Game { get; set; }

    }
}
