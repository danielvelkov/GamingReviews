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

        public Games(int id,string summary, string name,
            int user_id, byte[] image)
        {
            this.Entity_id = id;
            this.Summary = summary;
            this.Name = name;
            this.User_id = user_id;
            Image = image;
            Date = DateTime.Now;
        }

        [Required]
        [Key,ForeignKey("Entity")]
        public int Entity_id { get; set; }

        [Required]
        [StringLength(255)]
        public string Summary { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [ForeignKey("User")]
        public int User_id { get; set; }

        [Required]
        public byte[] Image { get; set; }

        public DateTime Date { get; set; }

        public virtual Entities Entity { get; set; }

        public virtual Users User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articles> Articles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
