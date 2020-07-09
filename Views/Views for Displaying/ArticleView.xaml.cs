using GamingReviews.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GamingReviews.Views.Views_for_Displaying
{
    /// <summary>
    /// Interaction logic for ArticleView.xaml
    /// </summary>
    public partial class ArticleView : UserControl,IView
    {
        public ArticleView()
        {
            InitializeComponent();
            commentTxtBox.Text = "Enter comment...";
            commentTxtBox.GotFocus += new System.Windows.RoutedEventHandler(RemoveText);
            commentTxtBox.LostFocus += new System.Windows.RoutedEventHandler(AddText);
        }

        public void RemoveText(object sender, EventArgs e)
        {
            if (commentTxtBox.Text == "Enter comment...")
            {
                commentTxtBox.Text = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(commentTxtBox.Text))
                commentTxtBox.Text = "Enter comment...";
        }
        
    }
}
