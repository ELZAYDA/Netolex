using BLL.Repositiries.Implementation;
using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using System;

namespace BLL.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbApplicationContext _dbcontext;

        public IMovieRepo movieRepo { get; set; } = null;
        public IGenreRepo genreRepo { get;  set; } = null;
        public IActorRepo actorRepo { get;  set; } = null;
        public IDirectorRepo directorRepo { get;  set; } = null;
        public IWatchListRepo watchListRepo { get;  set; } = null;
        public IReviewRepo reviewRepo { get; set; } = null;
        //public IMovieGenreRepo movieGenreRepo { get; set; } = null;

        public UnitOfWork(DbApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;

            // Assign repositories to properties, not local variables
            movieRepo = new MovieRepo(_dbcontext);
            genreRepo = new GenreRepo(_dbcontext);
            actorRepo = new ActorRepo(_dbcontext);
            directorRepo = new DirectorRepo(_dbcontext);
            watchListRepo = new WatchListRepo(_dbcontext);
            reviewRepo = new ReviewRepo(_dbcontext);
            //movieGenreRepo = new MovieGenreRepo(_dbcontext);
        }

        public async Task<int> Complete()
        {
            return  await _dbcontext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
          await  _dbcontext.DisposeAsync();
        }
    }
}
