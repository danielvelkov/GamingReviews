using GamingReviews.Core;
using GamingReviews.Core.IRepositories;
using GamingReviews.Core.Repositories;
using GamingReviews.Models;
using GamingReviews.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Persistance
{
    // Unit of work is used to commit changes to multiple tables in one transaction
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameNewsLetterContext _context;

        // TODO: could be made for specific repositories so its not that bulky
        // for example most of the cases i use are entities add then X type model with that entity ID 
        public UnitOfWork(GameNewsLetterContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Reviews = new ReviewsRepository(_context);
            Articles = new ArticlesRepository(_context);
            Games = new GamesRepository(_context);
            Comments = new CommentsRepository(_context);
            Logs = new LogsRepository(_context);
            Entities = new EntitiesRepository(_context);
            Votes = new VotesRepository(_context);
        }

        public IUserRepository Users{ get; private set; }
        public IReviewsRepository Reviews { get; private set; }
        public IArticlesRepository Articles { get; private set; }
        public IGamesRepository Games { get; private set; }
        public ICommentsRepository Comments { get; private set; }
        public ILogsRepository Logs { get; private set; }
        public IEntityRepository Entities { get; private set; }
        public IVotesRepository Votes { get; private set; }

        // basically save changes for the actions
        public int Complete()
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                return _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
        }


        // for simultanious entry if needed
        public Task<int> CompleteAsync()
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                return _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
