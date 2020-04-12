using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Models
{
    public class Review
    {
        string author;
        Uri imageURI;
        string title;
        string review;
        string game;
        string rating;

        public Review(string author, Uri imageURI, string review, string game, string rating,string title)
        {
            this.Title = title;
            this.Author = author;
            this.ImageURI = imageURI;
            this.Reveiw = review;
            this.Game = game;
            this.Rating = rating;
        }

        public string Author { get => author; set => author = value; }
        public Uri ImageURI { get => imageURI; set => imageURI = value; }
        public string Reveiw { get => review; set => review = value; }
        public string Game { get => game; set => game = value; }
        public string Rating { get => rating; set => rating = value; }
        public string Title { get => title; set => title = value; }
    }
}
