namespace GamingReviews.Models
{
    using GamingReviews.Interfaces;
    using GamingReviews.Persistance;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

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
                using (var UsersRepo = new UserRepository(new GameNewsLetterContext()))
                {
                    var author = UsersRepo.Get(this.User_id).UserName;
                    return author;
                }
            }
        }

        ObservableCollection<Comments> commentDiscussion = new ObservableCollection<Comments>();
        ObservableCollection<Votes> commentVotes = new ObservableCollection<Votes>();

        [NotMapped]
        public ObservableCollection<Comments> CommentDiscussion
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                {
                    // fills with comments if there is any
                    if (!commentDiscussion.Any())
                    {
                        commentDiscussion = unitOfWork.Entities.Get(this.Entity_Id).Target_Comment;
                    }
                    unitOfWork.Complete();
                }
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
        [NotMapped]
        public ObservableCollection<Votes> CommentVotes
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                {
                    if (!commentVotes.Any())
                    {
                        commentVotes = unitOfWork.Entities.Get(this.Entity_Id).Votes;
                        
                    }
                    unitOfWork.Complete();
                }
                return commentVotes;
            }
        }
        [NotMapped]
        public int VotesCount
        {
            get
            {
                var votes = 0;
                foreach (var vote in CommentVotes)
                {
                    if (vote.Reaction == Models.Reaction.Liked)
                        votes++;
                    else votes--;

                }
                return votes;
            }
        }
    }
}
