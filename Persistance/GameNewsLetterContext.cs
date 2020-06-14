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
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Votes> Votes { get; set; }

        // set their properties and relations here 
        // NOTE: you need to change the model classes first
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          // for specific stuff if you dont want to migrate
        }
    }
}
