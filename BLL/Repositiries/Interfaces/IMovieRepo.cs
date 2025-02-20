using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositiries.Interfaces
{
    public interface IMovieRepo : IGenericRepo<Movie>
    {
        public IQueryable<Movie> GetByName(string name);

        public Task<Movie> GetMoviesWithGenresAsync(int id);
        
        public Task<IEnumerable<Movie>> GetMoviesWithGenresAsync();

        public void UpdateMovieGenres(Movie movie, Movie existingMovie);

    }
}
