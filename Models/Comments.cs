namespace GamingReviews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        public int Id { get; set; }

        public int Article_id { get; set; }

        [Required]
        public string Content { get; set; }

        public int User_id { get; set; }

        public virtual Articles Articles { get; set; }

        public virtual Users Users { get; set; }
    }
}
