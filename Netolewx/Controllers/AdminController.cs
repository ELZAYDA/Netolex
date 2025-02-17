using AutoMapper;
using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Netolewx.ViewModels;
using Netolewx.ViewModels.MovieVM;
using Netolewx.ViewModels.MovieVM.MovieVM;

namespace Netolewx.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMovieRepo _movierepo;
        private readonly IMapper _mapper;
        private readonly DbApplicationContext _dbApplicationContext;
        private readonly IGenreRepo _genreRepo;
        //private readonly IMovieGenreRepo _movieGenreRepo;

        public AdminController(
            IMovieRepo movierepo, IMapper mapper, DbApplicationContext dbApplicationContext, IGenreRepo genreRepo /*IMovieGenreRepo movieGenreRepo*/)
        {
            _movierepo=movierepo;
            _mapper=mapper;
            _dbApplicationContext=dbApplicationContext;
            _genreRepo=genreRepo;
            //_movieGenreRepo=movieGenreRepo;
        }

        public IActionResult Index()
        {

            var movies = _movierepo.GetAll();
            if (movies == null)
                return NotFound();
            else
                return View(movies);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddMovieVM();

            // تحميل الأنواع من قاعدة البيانات
            model.GenreList = _genreRepo.GetAll().Select(g => new SelectListItem
            {
                Text = g.Name,  // اسم النوع
                Value = g.Id.ToString()  // قيمة النوع
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddMovieVM model)
        {
            if (ModelState.IsValid)
            {
                // إنشاء كائن الفيلم وإضافته إلى قاعدة البيانات

                var movie = _mapper.Map<AddMovieVM, Movie>(model);

                _movierepo.Add(movie);   // إضافة الفيلم أولًا

                // الآن يمكننا إضافة العلاقة بعد أن تم حفظ الفيلم وأصبح له Id
                if (model.SelectedGenres != null)
                {
                    foreach (var genreId in model.SelectedGenres)
                    {
                        var movieGenre = new MovieGenre
                        {
                            MovieId = movie.Id,  // الآن movie.Id يحتوي على القيمة الصحيحة
                            GenreId = genreId
                        };
                        _dbApplicationContext.MovieGenre.Add(movieGenre);  // إضافة العلاقة بين الفيلم والنوع
                    }

                    _dbApplicationContext.SaveChanges(); // حفظ العلاقات في قاعدة البيانات
                }

                return RedirectToAction("Index");  // إعادة التوجيه إلى قائمة الأفلام
            }
            // إعادة تحميل قائمة الأنواع عند حدوث خطأ
            model.GenreList = _genreRepo.GetAll()
                .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                .ToList();

            return View(model);  // إذا كان هناك خطأ في النموذج، نعرضه مرة أخرى
        }


        public IActionResult Details(int id, string name = "Details")
        {
            var entity = _movierepo.GetMoviesWithGenres(id);

            if (entity == null) // Check if the movie exists first
            {
                return NotFound(); // Return 404 if movie doesn't exist
            }
            var detailsMovie = _mapper.Map<Movie, DetailsEditMovieVM>(entity);



            return View(name, detailsMovie);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit(DetailsEditMovieVM entity)
        {
            if (ModelState.IsValid)
            {

                if (entity == null)
                {
                    return NotFound(); // Handle movie not found
                }


                var movie = _mapper.Map<DetailsEditMovieVM, Movie>(entity);
                _movierepo.Update(movie); // Save updates to DB
                return RedirectToAction("Index");
            }

            return View(entity); // Return form with validation errors
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var movie = _movierepo.Get(id); // Fetch movie first

            if (movie == null)
            {
                return NotFound(); // Movie doesn't exist, return 404
            }

            _movierepo.Delete(movie); // Delete only if found

            return RedirectToAction("Index");
        }

    }
}