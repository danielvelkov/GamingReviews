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

namespace GamingReviews.ViewModels
{
    public class ArticleViewModel:BaseViewModel
    {
        string commentText;
        string replyCommentText;
        ObservableCollection<Comments> commentSection = new ObservableCollection<Comments>();

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
                if (!commentSection.Any())
                {
                    using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                    {
                        Entities entity = unitOfWork.Entities.Get(GetSelectedArticle().Entity_Id);

                        //lazy loading
                        foreach (var comment in entity.Target_Comment)
                        {
                            commentSection.Add(comment);
                        }
                    }
                }
                return commentSection;
            }
            set
            {
                if (commentSection != value)
                {
                    commentSection = value;
                    NotifyPropertyChanged("CommentSection");
                }
            }
        }
        
        public string CommentText
        {
            get
            {
                if (commentText == null)
                    commentText = "Add comment...";
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
                if (replyCommentText == null)
                    replyCommentText = "Add comment...";


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
        #endregion

        #region commands
        
        ICommand addComment;
        ICommand viewReplies;
        ICommand addReply;
        
        public ICommand AddComment
        {
            get
            {
                if (addComment == null)
                    addComment = new RelayCommand<Object>(x =>
                      {
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
                              Debug.WriteLine(Entity.Entity_Id);
                              Debug.WriteLine(Comment.Entity_Id);
                              CommentSection.Add(Comment);
                          }
                      }, () => { return true; });
                return addComment;
            }
        }
        public ICommand ViewReplies
        {
            get
            {
                if (viewReplies == null)
                    viewReplies = new RelayCommand<Comments>(x =>
                    {
                        using (var unitofwork = new UnitOfWork(new GameNewsLetterContext()))
                        {

                        }

                    }, () => { return true; });
                return viewReplies;
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
                              CommentSection.Where(y=>y.Entity_Id== x.Entity_Id ).First().CommentDiscussion.Add(Comment);
                              NotifyPropertyChanged("CommentDiscussion");
                          }
                      }, () => { return true; });
                return addReply;
            }
        }
        #endregion

    }
}
