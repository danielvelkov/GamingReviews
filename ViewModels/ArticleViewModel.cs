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
    class ArticleViewModel:BaseViewModel
    {
        
        public string Author
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new DBContext()))
                {
                    Articles currentArticle = GetSelectedArticle();
                   Users user =unitOfWork.Users.Find(u => u.Id == currentArticle.User_id).FirstOrDefault();
                    if (user == null)
                    {
                        return "anonymouse";
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
                    return new BitmapImage(new Uri("C:\\Users\\Administrator\\Desktop\\Project\\GamingReviews\\res\\Images\\no image.png"));
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

        #region change to user page

        ICommand goToUserProfile;

        public ICommand GoToUserProfile
        {
            get
            {
                if (goToUserProfile == null)
                    goToUserProfile = new RelayCommand<Object>(ChangeToUserPage);
                return goToUserProfile;
            }
        }
        private void ChangeToUserPage()
        {

            // probably pass the current user too
            App.Current.MainWindow.Content = ViewModelsFactory.ViewModelType(ViewModelTypes.UserPageViewModel);
        }

        #endregion

        #region go to home page

        ICommand goToHomePage;

        public ICommand GoToHomePage
        {
            get
            {
                if (goToHomePage == null)
                    goToHomePage = new RelayCommand<Object>(ChangeToHomePage);
                return goToHomePage;
            }
        }
        private void ChangeToHomePage()
        {

            // probably pass the current user too
            App.Current.MainWindow.Content = ViewModelsFactory.ViewModelType(ViewModelTypes.HomePageViewModel);
        }

        #endregion

    }
}
