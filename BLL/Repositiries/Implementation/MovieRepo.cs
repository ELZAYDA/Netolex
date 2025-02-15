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

        public IQueryable<Movie> GetByName(string name)
                => _dbcontext.Movies.Where(m => m.Title.ToLower().Contains(name));

        public IEnumerable<Movie> GetMoviesWithGenres()
        {
            return _dbcontext.Movies
                .Include(m => m.MovieGenres) // تحميل العلاقة بين الأفلام والأنواع
                .ThenInclude(mg => mg.Genre) // تحميل تفاصيل النوع لكل فيلم
                .AsNoTracking()
                .ToList();
        }
    }
}
