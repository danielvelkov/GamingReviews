namespace GamingReviews.Models
{
    using GamingReviews.Persistance;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.IO;
    using System.Windows.Media.Imaging;

    public partial class Articles
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Articles()
        {
            Comments = new HashSet<Comments>();
        }

        public Articles(string name, int user_id, int game_id, string content, string header, byte[] image)
        {
            this.name = name;
            User_id = user_id;
            this.game_id = game_id;
            this.content = content;
            Header = header;
            Image = image;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        public int User_id { get; set; }

        public int game_id { get; set; }

        [Required]
        public string content { get; set; }

        [Required]
        public string Header { get; set; }

        public byte[] Image { get; set; }

        public virtual Games Games { get; set; }

        public virtual Users Users { get; set; }

        public string Author
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new DBContext()))
                {
                    var author = unitOfWork.Users.Get(User_id).UserName;
                    return author;
                }
            }
        }

        public string GameName
        {
            get
            {
                using (var unitOfWork = new UnitOfWork(new DBContext()))
                {
                    var gameName = unitOfWork.Games.Get(game_id).name;
                    return gameName;
                }
            }
        }

        public BitmapImage ImageSource
        {
            get
            {
                var imageData = Image;
                if (imageData == null || imageData.Length < 20)
                {
                    return new BitmapImage(new Uri("res/Images/no image.png", UriKind.Relative));
                }
                var image = new BitmapImage();
                using (var mem = new MemoryStream(imageData))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                return image;
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
