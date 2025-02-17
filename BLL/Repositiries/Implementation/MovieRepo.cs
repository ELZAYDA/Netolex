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

        public Movie GetMoviesWithGenres(int id)
        {
            return _dbcontext.Movies
                           .Include(m => m.MovieGenres) // تحميل العلاقة بين الأفلام والأنواع
                           .ThenInclude(mg => mg.Genre) // تحميل تفاصيل النوع لكل فيلم
                           .Where(m => m.Id == id) // إضافة شرط لتصفية الأفلام بناءً على الـ id
                           .AsNoTracking()
                           .FirstOrDefault();
                            // تحسين الأداء بعدم تتبع الكائنات
                            // تحويل النتيجة إلى قائمة        }
        }

        public IEnumerable<Movie> GetMoviesWithGenres()
        {
            return _dbcontext.Movies
                           .Include(m => m.MovieGenres) // تحميل العلاقة بين الأفلام والأنواع
                           .ThenInclude(mg => mg.Genre) // تحميل تفاصيل النوع لكل فيلم
                           .AsNoTracking() // تحسين الأداء بعدم تتبع الكائنات
                           .ToList(); // تحويل النتيجة إلى قائمة        }
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
