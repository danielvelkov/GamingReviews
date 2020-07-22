using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GamingReviews.ViewModels.Handling_Entity_Addition
{
    class AddGameViewModel : BaseViewModel
    { 
    public AddGameViewModel()
    {
        Game = new Games();
        BitmapImage defaultImage = new BitmapImage(new Uri("pack://application:,,,/res/Images/no image.png"));
        GamePic = BitMapToByteArray.Convert(defaultImage);
    }

    #region fields
    byte[] gamePic;
    Games game;
    #endregion

    #region parameters
    public Games Game
    {
        get
        {
            return game;
        }
        set
        {
            if (game != value)
            {
                game = value;

            }
        }
    }
    public byte[] GamePic
    {
        get { return gamePic; }
        set
        {
            if (gamePic != value)
            {
                gamePic = value.ToArray();
                NotifyPropertyChanged("GamePic");
            }
        }
    }
    #endregion

    #region commands
    ICommand selectPicture;
    ICommand addGame;
       

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
                            MessageBox.Show("image too big", "cant upload", MessageBoxButton.OK);
                        }
                        else
                        {
                            GamePic = BitMapToByteArray.Convert(img);
                        }
                    }

                });

            }
            return selectPicture;
        }
    }
    public ICommand AddGame
    {
        get
        {
            if (addGame == null)
            {
                addGame = new RelayCommand<Object>(x =>
                {
                    using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                    {
                        var Entity = new Entities();
                        unitOfWork.Entities.Add(Entity);
                        unitOfWork.Complete();

                        Game.Image = GamePic;
                        Game.User_id = GetCurrentUser().Id;
                        Game.Date = DateTime.Now;

                        unitOfWork.Games.Add(Game);

                        Game.Entity = Entity;
                        unitOfWork.Complete();

                        MessageBox.Show("Game added succesfully", "Game added", MessageBoxButton.OK);

                        SetSelectedGame(Game);
                        Mediator.NotifyColleagues("ChangeView", ViewModelTypes.GamePageViewModel);

                    }
                }, () =>
                {
                    //{
                    //    if(String.IsNullOrEmpty(Game.Header))
                    //    return false;
                    //    else if (String.IsNullOrEmpty(Game.Name))
                    //        return false;
                    //    else if (String.IsNullOrEmpty(Game.Content))
                    //        return false;

                    return true;
                });
            }
            return addGame;
        }
    }
    #endregion
}
}
