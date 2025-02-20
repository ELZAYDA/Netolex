using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositiries.Interfaces
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IMovieRepo movieRepo { get; set; }
        IGenreRepo genreRepo { get; set; }
        IActorRepo actorRepo { get; set; }
        IDirectorRepo directorRepo { get; set; }
        IWatchListRepo watchListRepo { get; set; }
        IReviewRepo reviewRepo { get; set; }
        //IMovieGenreRepo movieGenreRepo { get; set; }
        public Task<int> Complete();
    }
}
