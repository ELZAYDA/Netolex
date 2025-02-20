using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Repositiries.Implementation
{
    public class MovieRepo : GenericRepo<Movie>, IMovieRepo
    {
        public MovieRepo(DbApplicationContext dbcontext) : base(dbcontext)
        {
        }

        public  IQueryable<Movie> GetByName(string name)
        {
            return  _dbcontext.Movies.Where(m => m.Title.ToLower().Contains(name));
        }

        public async Task<Movie> GetMoviesWithGenresAsync(int id)
        {
            // Await the query to fetch a movie with genres by ID
            return await _dbcontext.Movies
                .Include(m => m.MovieGenres) // Load the relationship between movies and genres
                .ThenInclude(mg => mg.Genre) // Load genre details for each movie
                .Where(m => m.Id == id) // Filter by movie ID
                .AsNoTracking() // Improve performance by not tracking entities
                .FirstOrDefaultAsync(); // Use FirstOrDefaultAsync for asynchronous fetching
        }

        public async Task<IEnumerable<Movie>> GetMoviesWithGenresAsync()
        {
            // Await the query to fetch all movies with their genres
            return await _dbcontext.Movies
                .Include(m => m.MovieGenres) // Load the relationship between movies and genres
                .ThenInclude(mg => mg.Genre) // Load genre details for each movie
                .AsNoTracking() // Improve performance by not tracking entities
                .ToListAsync(); // Use ToListAsync to execute the query asynchronously
        }


        public void UpdateMovieGenres(Movie movie, Movie existingMovie)
        {
            // أولاً، نقوم بحذف أي علاقة قديمة
            existingMovie.MovieGenres.Clear();

            // ثم نقوم بإضافة العلاقات الجديدة
            foreach (var movieGenre in movie.MovieGenres)
            {
                existingMovie.MovieGenres.Add(movieGenre); // إضافة علاقات جديدة
            }
        }

       
    }
}
