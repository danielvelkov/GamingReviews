using GamingReviews.ViewModels;
using GamingReviews.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GamingReviews
{
    public enum ViewModelTypes
    {
        LoginPageViewModel = 1,
        HomePageViewModel = 2,
        UserPageViewModel = 3,
        GamePageViewModel = 4,
        RegisterPageViewModel = 5,
        ArticleViewModel=6,
        Default = 999
    };

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            // set it to the main window
            MainWindowView view = new MainWindowView();
            MainViewModel viewModel = new MainViewModel();

            view.DataContext = viewModel;
            viewModel.View = view;
            
            view.Show();

            base.OnStartup(e);
        }
    }
}
