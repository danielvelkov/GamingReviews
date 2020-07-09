﻿using GamingReviews.ViewModels;
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
        GameReviewsViewModel=7,
        ReviewViewModel=8,
        AddArticleViewModel=9,
        AddGameViewModel=10,
        AddReviewViewModel=11,
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
            view.Show();

            base.OnStartup(e);
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occured" + e.Exception.Message, "exception", MessageBoxButton.OK);
            e.Handled = true;
        }
    }
}
