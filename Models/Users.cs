namespace GamingReviews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public enum UserType
    {
        ADMIN,
        USER
    }

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Articles = new HashSet<Articles>();
            Comments = new HashSet<Comments>();
            Games = new HashSet<Games>();
            Reviews = new HashSet<Reviews>();
            Logs = new HashSet<Logs>();
            Votes = new HashSet<Votes>();
        }

        public Users(string username, UserType usertype,
            string password,
            byte[] image, string email)
        {
            UserName = username;
            UserType = usertype;
            this.Password = password;
            this.Image = image;
            Email = email;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }
        
        public UserType UserType { get; set; }

        [StringLength(10)]
        public string Password { get; set; }

        public byte[] Image { get; set; }

        [Required]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articles> Articles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Games> Games { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviews> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Logs> Logs { get; set; }

        public virtual ICollection<Votes> Votes { get; set; }
    }
}
