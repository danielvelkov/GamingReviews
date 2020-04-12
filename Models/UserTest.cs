using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Models
{
    public enum UserType { USER = 1, ADMIN = 2 };

    public class User
    {
        //private int id;
        private string name;
        //private Uri image;

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public User(string name, string password, string email)
        {
            Name = name;
            Password = password;
        }

        public string Name { get => name; set => name = value; }
        public string Password { get; set; }
    }
}
