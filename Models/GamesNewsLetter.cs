namespace GamingReviews.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GamesNewsLetter : DbContext
    {
        public GamesNewsLetter()
            : base("name=GamesNewsLetter")
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articles>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Articles>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<Articles>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Articles)
                .HasForeignKey(e => e.Article_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comments>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Games>()
                .Property(e => e.summary)
                .IsUnicode(false);

            modelBuilder.Entity<Games>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Games>()
                .HasMany(e => e.Articles)
                .WithRequired(e => e.Games)
                .HasForeignKey(e => e.User_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Games>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Games)
                .HasForeignKey(e => e.Game_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reviews>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Reviews>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UserType)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.password)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Articles)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Games)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_id)
                .WillCascadeOnDelete(false);
        }
    }
}
