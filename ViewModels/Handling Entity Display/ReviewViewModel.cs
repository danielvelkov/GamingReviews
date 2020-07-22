using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GamingReviews.ViewModels.Handling_Entity_Display
{
    public class ReviewViewModel:BaseViewModel
    {
        public ReviewViewModel()
        {
            commentSection = new ObservableCollection<Comments>();
            commentSection = Review.CommentSection;

            ReviewId = GetSelectedReview().Entity_id;
            UserId = GetCurrentUser().Id;
        }

        #region variables

        int ReviewId;
        int UserId;
        string commentText;
        string replyCommentText;
        ObservableCollection<Comments> commentSection;
        int reviewVotes;
        

        #endregion

        #region parameters

        public Reviews Review
        {
            get
            {
                return this.GetSelectedReview();
            }
        }
        
        public ObservableCollection<Comments> CommentSection
        {
            get
            {
                return commentSection;
            }
        }

        public int ReviewVotes
        {
            get
            {
                reviewVotes = 0;
                foreach (var vote in Review.Votes)
                {
                    if (vote.Reaction == Models.Reaction.Liked)
                        reviewVotes++;
                    else reviewVotes--;

                }
                return reviewVotes;
            }
            set
            {
                if (reviewVotes != value)
                {
                    reviewVotes = value;
                    NotifyPropertyChanged("Votes");
                }
            }
        }

        public string CommentText
        {
            get
            {
                return commentText;
            }
            set
            {
                if (commentText != value)
                {
                    commentText = value;
                    NotifyPropertyChanged("CommentText");
                }
            }
        }

        public string ReplyCommentText
        {
            get
            {
                return replyCommentText;
            }
            set
            {
                if (replyCommentText != value)
                {
                    replyCommentText = value;
                    NotifyPropertyChanged("ReplyCommentText");
                }
            }
        }
        Reaction reaction;
        public Reaction Reaction
        {
            get
            {
                using (var unitofwork = new UnitOfWork(new GameNewsLetterContext()))
                {
                    if (unitofwork.Votes.HasVoted(Review.Entity_id, GetCurrentUser().Id))
                    {
                        reaction = unitofwork.Votes.GetReaction(Review.Entity_id, GetCurrentUser().Id);
                        return reaction;
                    }

                }

                return Reaction.None;

            }
            set
            {
                if (reaction != value)
                {
                    reaction = value;
                    (VoteReview as RelayCommand<object>).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region commands

        ICommand addComment;
        ICommand addReply;
        ICommand voteReview;
        ICommand likeComment;
        ICommand dislikeComment;
        

        public ICommand AddComment
        {
            get
            {
                if (addComment == null)
                    addComment = new RelayCommand<Object>(x =>
                    {
                        // we use unit of work instead of repo so we can batch both adds together
                        using (var unitofwork = new UnitOfWork(new GameNewsLetterContext()))
                        {
                            var Entity = new Entities();
                            unitofwork.Entities.Add(Entity);
                            unitofwork.Complete();
                            var Comment = new Comments(GetSelectedReview().Entity_id,
                                CommentText, GetCurrentUser().Id);
                            unitofwork.Comments.Add(Comment);
                            Comment.Entity = Entity;
                            unitofwork.Complete();
                            CommentSection.Add(Comment);
                            CommentText = "";
                        }
                    });
                return addComment;
            }
        }
        public ICommand AddReply
        {
            get
            {
                if (addReply == null)
                    addReply = new RelayCommand<Comments>(x =>
                    {
                        using (var unitofwork = new UnitOfWork(new GameNewsLetterContext()))
                        {
                            var Entity = new Entities();
                            unitofwork.Entities.Add(Entity);
                            unitofwork.Complete();
                            var Comment = new Comments(x.Entity_Id,
                                ReplyCommentText, GetCurrentUser().Id);
                            unitofwork.Comments.Add(Comment);
                            Comment.Entity = Entity;
                            unitofwork.Complete();
                            CommentSection.Where(y => y.Entity_Id == x.Entity_Id).First().CommentDiscussion.Add(Comment);
                            ReplyCommentText = "";
                        }
                    });
                return addReply;
            }
        }
        public ICommand VoteReview
        {
            get
            {
                if (voteReview == null)
                    voteReview = new RelayCommand<object>(x =>
                    {
                        using (var unitofwork = new UnitOfWork(new GameNewsLetterContext()))
                        {
                            if (unitofwork.Votes.HasVoted(ReviewId, UserId))
                            {
                                unitofwork.Votes.ChangeVote(ReviewId, UserId, (Reaction)x);
                            }
                            else
                            {
                                Votes vote = new Votes(
                                ReviewId, UserId,
                                (Reaction)x);
                                unitofwork.Votes.Add(vote);

                                unitofwork.Complete();

                            }
                            NotifyPropertyChanged("ReviewVotes");
                            Reaction = (Reaction)x;
                        }
                    }, () =>
                    {
                        //idk idk i just wanted to disable the option that was selected
                        // how DO I DO THAT, do i need different command?
                        if (Reaction != Models.Reaction.None)
                        {
                            return false;
                        }

                        return true;
                    });
                return voteReview;
            }
        }
        public ICommand DislikeComment
        {
            get
            {
                if (dislikeComment == null)
                {
                    dislikeComment = new RelayCommand<Comments>(x =>
                    {
                        using (var unitofwork = new UnitOfWork(new GameNewsLetterContext()))
                        {
                            if (unitofwork.Votes.HasVoted(x.Entity_Id,UserId))
                            {
                                // dont do anything... ?
                            }
                            else
                            {
                                Votes vote = new Votes(
                                x.Entity_Id, UserId,
                                Reaction.Disliked);
                                unitofwork.Votes.Add(vote);
                                unitofwork.Complete();
                                CommentSection.Where(y => y.Entity_Id == x.Entity_Id).First().CommentVotes.Add(vote);
                                
                            }
                        }
                    });

                }
                return dislikeComment;
            }
        }

        public ICommand LikeComment
        {
            get
            {
                if (likeComment == null)
                {
                    likeComment = new RelayCommand<Comments>(x =>
                    {
                        using (var unitofwork = new UnitOfWork(new GameNewsLetterContext()))
                        {
                            if (unitofwork.Votes.HasVoted(x.Entity_Id, UserId))
                            {
                               // dont do anything... ?
                            }
                            else
                            {
                                Votes vote = new Votes(
                                x.Entity_Id, UserId,
                                Reaction.Liked);
                                unitofwork.Votes.Add(vote);
                                unitofwork.Complete();
                                CommentSection.Where(y => y.Entity_Id == x.Entity_Id).First().CommentVotes.Add(vote);
                                
                            }
                        }
                    });

                }
                return likeComment;
            }
        }

        #endregion

    }
}
