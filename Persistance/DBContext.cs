using System;
using System.Collections.Generic;
using System.Data.Entity;       //   <------
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Models
{

    // needs nuget package of entity framework to work 
    public class GameNewsLetterContext : DbContext
    {

        // for this to work we add the refference Configuration.dll in refferences
        // also in the ConnectionStrings[ <Insert connection string name here > ]
        public GameNewsLetterContext()
            : base("name=GamesNewsLetter")
        {
        }

        // these are all tables in the database
        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        // set their properties and relations here 
        // NOTE: you need to change the model classes first
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
