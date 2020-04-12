namespace GamingReviews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Games
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Games()
        {
            Articles = new HashSet<Articles>();
            Reviews = new HashSet<Reviews>();
        }

        public Games(string summary, string name, int user_id, byte[] image)
        {
            this.summary = summary;
            this.name = name;
            this.User_id = user_id;
            Image = image;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string summary { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        public int User_id { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articles> Articles { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
