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

        public Movie GetMoviesWithGenres(int id);
        
        public IEnumerable<Movie> GetMoviesWithGenres();

        public void UpdateMovieGenres(Movie movie, Movie existingMovie);

    }
}
