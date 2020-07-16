using GamingReviews.Helper;
using GamingReviews.Models;
using GamingReviews.Persistance;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GamingReviews.ViewModels
{
    class UserPageViewModel : BaseViewModel
    {
        public UserPageViewModel()
        {
            Users CurrentUser = base.GetCurrentUser();
            UserName = CurrentUser.UserName;
            Email = CurrentUser.Email;
            UserName = CurrentUser.UserName;
            Type = CurrentUser.UserType;
            ProfilePic = CurrentUser.Image;
            
        }

        #region fields

        string userName;
        string password;
        string confirmPassword;
        string email;
        UserType type;
        byte[] profilePic;
        string errorMsg;
        List<Logs> logs;
        

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

        public UserType Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        public byte[] ProfilePic
        {
            get { return profilePic; }
            set
            {
                if (profilePic != value)
                {
                    profilePic = value.ToArray();
                    NotifyPropertyChanged("ProfilePic");
                }
            }
        }

        public string ErrorMsg
        {
            get { return errorMsg; }
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    NotifyPropertyChanged("ErrorMsg");
                }
            }
        }

        public List<Logs> Logs
        {
            get
            {
                return logs;
            }
            set
            {
                if (logs != value)
                {
                    logs = value;
                    NotifyPropertyChanged("Logs");
                }
            }
        }
        #endregion

        ICommand saveChanges;
        ICommand selectPicture;

        

        public ICommand SaveChanges
        {
            get
            {
                if (saveChanges == null)
                    saveChanges = new RelayCommand<Object>(x =>
                    {
                        if (Password == ConfirmPassword)
                        {
                            if (Password.Length >= 8 && Password.Length < 11)
                            {
                                using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                                {
                                    unitOfWork.Users.UpdatePassword(this.GetCurrentUser().Id, Password);
                                    this.GetCurrentUser().Password = Password;
                                }
                                MessageBox.Show("Password changed", "Success", MessageBoxButton.OK);
                            }
                            else
                            {
                                ErrorMsg = "Password should be between 8 and 10 symbols! ";
                            }
                        }
                        else
                        {
                            ErrorMsg = "new password doesnt match";
                        }
                        if (!ByteArrayCompare(ProfilePic, this.GetCurrentUser().Image))
                        {
                            using (var unitOfWork = new UnitOfWork(new GameNewsLetterContext()))
                            {
                                unitOfWork.Users.UpdateImage(this.GetCurrentUser().Id, ProfilePic);
                                this.GetCurrentUser().Image = ProfilePic.ToArray();
                            }
                            MessageBox.Show("Picture Changed", "Success", MessageBoxButton.OK);
                        }
                        Mediator.NotifyColleagues("ChangeView", ViewModelTypes.HomePageViewModel);
                    

                    }
                    );
                return saveChanges;
            }
        }

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
                                 ErrorMsg = "wrong picture size!";
                             }
                             else
                             {
                                 ProfilePic = BitMapToByteArray.Convert(img);
                             }
                         }
                         
                     });
                    
                }
                return selectPicture;
            }
        }

        static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            return a1.SequenceEqual(a2);
        }
    }

}
