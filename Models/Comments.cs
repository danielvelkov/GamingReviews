namespace GamingReviews.Models
{
    using GamingReviews.Persistance;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        public Comments()
        {

        }

        public Comments(Entities Entity, int targetId,
            string content, int userId)
        {
            this.Entity = Entity;
            TargetEntity_Id = targetId;
            Content = content;
            User_id = userId;
            Date = DateTime.Now;
        }

        public Comments(int targetId,
            string content, int userId)
        {
            TargetEntity_Id = targetId;
            Content = content;
            User_id = userId;
            Date = DateTime.Now;
        }

        [Key, ForeignKey("Entity")]
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

        // use [NotMapped] for properties you dont want added in ef

        [NotMapped]
        public string Author
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                {
                    var author = unitOfWork.Users.Get(User_id).UserName;
                    return author;
                }
            }
        }
        [NotMapped]
        public ObservableCollection<Comments> CommentDiscussion
        {
            get
            {
                ObservableCollection<Comments> commentDiscussion;

                //using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                //{
                //    Entities entity = unitOfWork.Entities.Get(Entity_Id);

                //    //with lazy loading
                //    commentDiscussion = new ObservableCollection<Comments>();
                //    foreach (var comment in entity.Target_Comment)
                //        commentDiscussion.Add(comment);
                //}
                commentDiscussion = new ObservableCollection<Comments>();
                commentDiscussion.Add(new Comments(this.Entity_Id, "tests", 1));
                return commentDiscussion;
            }
        }

        [NotMapped]
        public byte[] ProfilePic
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                {
                    byte[] image = unitOfWork.Users.Get(User_id).Image;
                    return image;
                }
            }
        }

    }
}
