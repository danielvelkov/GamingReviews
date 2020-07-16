using GamingReviews.Helper;
using GamingReviews.Interfaces;
using GamingReviews.Models;
using GamingReviews.Persistance;
using GamingReviews.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GamingReviews.ViewModels
{
    class RegisterPageViewModel:BaseViewModel
    {
        #region fields

        string userName;
        string password;
        string confirmPassword;
        string email;
        string errorMsg;
        string fileName;
        BitmapImage displayedImage;
        #endregion

        #region parameters

        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    NotifyPropertyChanged("ConfirmPassword");
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        public string ErrorMsg
        {
            get
            {
                if (errorMsg == null)
                {
                    errorMsg = "Password empty";
                }
                return errorMsg;
            }
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    
                    NotifyPropertyChanged("ErrorMsg");
                }
            }
        }

        public BitmapImage DisplayedImage
        {
            get
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    displayedImage = new BitmapImage();
                    displayedImage.BeginInit();
                    displayedImage.CacheOption = BitmapCacheOption.OnLoad;
                    displayedImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    displayedImage.UriSource = new Uri(fileName);
                    displayedImage.DecodePixelWidth = 200;
                    displayedImage.EndInit();
                }
                else
                {
                    return displayedImage= new BitmapImage(new Uri("pack://application:,,,/res/Images/no image.png"));
                }
                return displayedImage;
            }
            set
            {
                if (displayedImage != value)
                {
                    displayedImage = value;
                    NotifyPropertyChanged("DisplayedImage");
                }
            }
        }


        #endregion

        #region commands

        ICommand registerUser;
        ICommand selectImage;

        public ICommand RegisterUser
        {
            get
            {
                if (registerUser == null)
                    registerUser = new RelayCommand<Object>(x=>
                    {// register user service
                        if ((Password == ConfirmPassword) && Password.Length < 11)
                        {

                            using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                            {
                                if (!unitOfWork.Users.DoesUserExist(UserName))
                                {
                                    if (!unitOfWork.Users.DoesEmailExist(Email))
                                    {
                                        // Image conversion
                                        byte[] imageData = BitMapToByteArray.Convert(DisplayedImage);
                                        Users newUser = new Users(UserName,
                                            UserType.ADMIN, Password, imageData, Email);
                                        unitOfWork.Users.Add(newUser);
                                        MessageBox.Show("Registration complete. Congratulations!"
                                            , "success", MessageBoxButton.OK);

                                    }

                                    else ErrorMsg = "email already in use";
                                }
                                else
                                {
                                    // set the error message to "user already exists"
                                    ErrorMsg = "User already exists";
                                }
                                unitOfWork.Complete();
                            }
                        }
                        else
                        {
                            if (Password.Length < 10 && Password.Length > 8)
                                ErrorMsg = "Passwords dont match";
                            ErrorMsg = "Password must be between 8 and 10 chars";
                        }

                    }
                    );
                return registerUser;
            }
        }

        public ICommand SelectImage
        {
            get
            {
                if (selectImage == null)
                {
                    selectImage = new RelayCommand<Object>(x =>
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
                             fileName = FileSelectDialog.FileName;
                             //checks if image is 500x500 and under 256kb
                             if (
                             CheckImageSize(new Uri(fileName))
                             == true)
                             {
                                 DisplayedImage = new BitmapImage(new Uri(fileName));
                             }
                             else ErrorMsg = "wrong picture size!";
                         }
                     });
                }
                return selectImage;
            }
        }

        #endregion

        #region methods

        bool CheckImageSize(Uri filePath)
        {
            BitmapImage image = new BitmapImage(filePath);
            if(image.PixelWidth==500 && image.PixelHeight==500)
            {
                return true;
            }
            return false;
        }
        #endregion

    }

}
