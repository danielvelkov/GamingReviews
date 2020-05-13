using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using System;
using System.Collections.Generic;
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
                return this.GetSelectedArticle().name;
            }
        }

        public string Content
        {
            get
            {
                return this.GetSelectedArticle().content;
            }
        }

        public string GameName
        {
            get
            {
                return this.GetSelectedArticle().GameName;
            }
        }

        public BitmapImage Image
        {
            get
            {
                var imageData = GetSelectedArticle().Image;
                if(imageData==null || imageData.Length < 20)
                {
                    // taken from here: https://stackoverflow.com/questions/350027/setting-wpf-image-source-in-code

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
        #endregion

        #region commands

        ICommand goToUserProfile;
        ICommand goToHomePage;

        public ICommand GoToUserProfile
        {
            get
            {
                if (goToUserProfile == null)
                    goToUserProfile = new RelayCommand<Object>(x=>
                    {
                        Mediator.NotifyColleagues("ChangeView", ViewModelTypes.UserPageViewModel);
                    
                    });
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

                    });
                return goToHomePage;
            }
        }

        #endregion

    }
}
