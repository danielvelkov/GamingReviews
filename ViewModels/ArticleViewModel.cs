using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using System;
using System.Collections.Generic;
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

        #region parameters
        public string Author
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                {
                    Articles currentArticle = GetSelectedArticle();
                   Users user =unitOfWork.Users.Find(u => u.Id == currentArticle.User_id).FirstOrDefault();
                    if (user == null)
                    {
                        return "anonymous";
                    }
                    else
                        return user.UserName;
                }
            }
        }

        public string Name
        {
            get
            {
                return this.GetSelectedArticle().Name;
            }
        }

        public string Content
        {
            get
            {
                return this.GetSelectedArticle().Content;
            }
        }

        public string GameName
        {
            get
            {
                return this.GetSelectedArticle().GameName;
            }
        }

        public List<byte[]> Images
        {
            get
            {
                return new List<byte[]>();
            }
        }

        public List<Comments> CommentSection
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                {
                    List<Comments> ArticleComments = unitOfWork.Comments.FindAllComments(this.GetSelectedArticle().Entity_Id);
                    return ArticleComments;
                }
            }
        }

        public BitmapImage Image
        {
            get
            {
                var imageData = GetSelectedArticle().Image;
                if(imageData==null || imageData.Length < 20)
                {
                   
                    return new BitmapImage(new Uri(@"/GamingReviews;component/res/Images/no image.png",UriKind.Relative));
                }
                var image = new BitmapImage();
                using(var mem= new MemoryStream(imageData))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                return image;
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
        #endregion

        #region commands

        ICommand goToUserProfile;
        ICommand goToHomePage;
        ICommand addComment;

        public ICommand GoToUserProfile
        {
            get
            {
                if (goToUserProfile == null)
                    goToUserProfile = new RelayCommand<Object>(x=>
                    {
                        Mediator.NotifyColleagues("ChangeView", ViewModelTypes.UserPageViewModel);
                    
                    }, () => { return true; });
                return goToUserProfile;
            }
        }

        public ICommand GoToHomePage
        {
            get
            {
                if (goToHomePage == null)
                    goToHomePage = new RelayCommand<Object>(x =>
                    {
                        Mediator.NotifyColleagues("ChangeView", ViewModelTypes.HomePageViewModel);

                    },()=> { return true; });
                return goToHomePage;
            }
        }

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

                          }
                      }, () => { return true; });
                return addComment;
            }
        }
        #endregion

    }
}
