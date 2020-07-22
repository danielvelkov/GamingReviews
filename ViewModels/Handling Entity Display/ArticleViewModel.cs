using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GamingReviews.ViewModels.Handling_Entity_Display
{
    public class ArticleViewModel:BaseViewModel
    {

        public ArticleViewModel()
        {
            commentSection = new ObservableCollection<Comments>();
            commentSection = Article.CommentSection;
        }

        #region variables

        string commentText;
        string replyCommentText;
        ObservableCollection<Comments> commentSection;
        int votes;

        #endregion

        #region parameters

        public Articles Article
        {
            get
            {
                return this.GetSelectedArticle();
            }
        }

        // TODO: image changing? multiple images per article?
        public List<byte[]> Images
        {
            get
            {
                return new List<byte[]>();
            }
        }

        public ObservableCollection<Comments> CommentSection
        {
            get
            {
                return commentSection;
            }
        }

        public int Votes
        {
            get
            {
                votes = 0;
                foreach(var vote in Article.Votes)
                {
                    if (vote.Reaction == Models.Reaction.Liked)
                        votes++;
                    else votes--;
                    
                }
                return votes;
            }
            set
            {
                if (votes != value)
                {
                    votes = value;
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
                if(replyCommentText!=value)
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
                    if (unitofwork.Votes.HasVoted(Article.Entity_Id, GetCurrentUser().Id))
                    {
                        reaction = unitofwork.Votes.GetReaction(Article.Entity_Id, GetCurrentUser().Id);
                        return reaction;
                    }
                    
                }
                
                return Reaction.None;
                
            }
            set
            {
                if (reaction != value){
                    reaction = value;
                    (VoteArticle as RelayCommand<object>).RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region commands
        
        ICommand addComment;
        ICommand addReply;
        ICommand voteArticle;
        //ICommand voteComment;
        
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
                              var Comment = new Comments(GetSelectedArticle().Entity_Id,
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
        public ICommand VoteArticle
        {
            get
            {
                
                int ArticleId = GetSelectedArticle().Entity_Id;
                int UserId = GetCurrentUser().Id;

                if (voteArticle == null)
                    voteArticle = new RelayCommand<object>(x =>
                     {
                         using (var unitofwork = new UnitOfWork(new GameNewsLetterContext()))
                         {
                             if(unitofwork.Votes.HasVoted(ArticleId, UserId))
                             {
                                 unitofwork.Votes.ChangeVote(ArticleId,UserId,(Reaction)x);
                             }
                             else
                             {
                                 Votes vote = new Votes(
                                 ArticleId, UserId,
                                 (Reaction)x);
                                 unitofwork.Votes.Add(vote);
                                 
                                 unitofwork.Complete();
                                 
                             }
                             NotifyPropertyChanged("Votes");
                             Reaction = (Reaction)x;
                         }
                     },()=> 
                     {
                         //idk idk i just wanted to disable the option that was selected
                         // how DO I DO THAT, do i need different command?
                         if (Reaction != Models.Reaction.None)
                         {
                             return false;
                         }

                         return true;
                     });
                return voteArticle;
            }
        }
        //public ICommand VoteComment
        //{
        //    get
        //    {
        //        return voteComment;
        //    }
        //}

        #endregion

    }
}
