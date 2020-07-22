using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GamingReviews.ViewModels.Handling_Entity_Addition
{
    class AddArticleViewModel : BaseViewModel
    {
        public AddArticleViewModel()
        {
            Article = new Articles();
            BitmapImage defaultImage = new BitmapImage(new Uri("pack://application:,,,/res/Images/no image.png"));
            ArticlePic = BitMapToByteArray.Convert(defaultImage);
        }

        #region fields

        Articles article;
        List<Games> games;
        Games selectedGame;
        int game_id;
        byte[] articlePic;
        #endregion

        #region parameters
        public Articles Article
        {
            get
            {
                return article;
            }
            set
            {
                if (article != value)
                {
                    article = value;
                    
                }
            }
        }
        public List<Games> GamesList
        {
            get
            {
                if (games == null)
                {
                    using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                    {
                        games = new List<Games>();
                        games.AddRange(unitOfWork.Games.GetAll());
                    }
                }

                return games;
            }
        }
        public Games SelectedGame
        {
            get
            {
                return selectedGame;
            }
            set
            {
                selectedGame = value;
            }
        }
        public int Game_id
        {
            get
            {
                game_id = SelectedGame.Entity_id;
                return game_id;
            }
        }
        public byte[] ArticlePic
        {
            get { return articlePic; }
            set
            {
                if (articlePic != value)
                {
                    articlePic = value.ToArray();
                    NotifyPropertyChanged("ArticlePic");
                }
            }
        }
        #endregion

        #region commands
        ICommand selectPicture;
        ICommand addArticle;

        public ICommand SelectPicture
        {
            get
            {
                if (selectPicture == null)
                {
                    selectPicture = new RelayCommand<Object>(x =>
                    {
                        OpenFileDialog FileSelectDialog = new OpenFileDialog
                        {
                            Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) |" +
                        " *.jpg; *.jpeg; *.jpe; *.jfif; *.png",
                            Multiselect = false,
                            Title = "Select your profile pic..."
                        };
                        if (FileSelectDialog.ShowDialog() == true)
                        {
                            var fileName = FileSelectDialog.FileName;
                            //checks if image is 500x500 and under 256kb
                            var img = new BitmapImage(new Uri(fileName));
                            if (img.PixelWidth > 500)
                            {

                            }
                            else
                            {
                                ArticlePic = BitMapToByteArray.Convert(img);
                            }
                        }

                    });

                }
                return selectPicture;
            }
        }
        public ICommand AddArticle
        {
            get
            {
                if (addArticle == null)
                {
                    addArticle = new RelayCommand<Object>(x =>
                    {
                        using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                        {
                            var Entity = new Entities();
                            unitOfWork.Entities.Add(Entity);
                            unitOfWork.Complete();

                            Article.Image = ArticlePic;
                            Article.User_id = GetCurrentUser().Id;
                            Article.Game_id = Game_id;
                            Article.Date = DateTime.Now;

                            unitOfWork.Articles.Add(Article);

                            Article.Entity = Entity;
                            unitOfWork.Complete();

                            MessageBox.Show("Article added succesfully", "Article added", MessageBoxButton.OK);

                            SetSelectedArticle(Article);
                            Mediator.NotifyColleagues("ChangeView", ViewModelTypes.ArticleViewModel);

                        }
                    }, () =>
                    { 
                     //{
                     //    if(String.IsNullOrEmpty(Article.Header))
                     //    return false;
                     //    else if (String.IsNullOrEmpty(Article.Name))
                     //        return false;
                     //    else if (String.IsNullOrEmpty(Article.Content))
                     //        return false;

                         return true;
                     });
                }
                return addArticle;
            }
        }
        #endregion
    }
} 
