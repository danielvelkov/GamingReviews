namespace GamingReviews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {

        public Comments(Entities Entity,int targetId,
            string content,int userId)
        {
            this.Entity = Entity;
            TargetEntity_Id = targetId;
            Content = content;
            User_id = userId;
            Date = DateTime.Now;
        }

        public Comments( int targetId,
            string content, int userId)
        {
            TargetEntity_Id = targetId;
            Content = content;
            User_id = userId;
            Date = DateTime.Now;
        }

        [Key,ForeignKey("Entity")]
        public int Entity_Id { get; set; }

        [ForeignKey("TargetEntity")]
        public int TargetEntity_Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int User_id { get; set; }
        
        public virtual Entities Entity { get; set; }

        public virtual Entities TargetEntity { get; set; }

        [ForeignKey("User_id")]
        public virtual Users User { get; set; }

        
    }
}
