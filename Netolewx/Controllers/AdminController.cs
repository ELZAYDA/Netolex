using AutoMapper;
using BLL.Repositiries.Implementation;
using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Netolewx.ViewModels;
using Netolewx.ViewModels.MovieVM;
using Netolewx.ViewModels.MovieVM.MovieVM;
using System.Drawing;

namespace Netolewx.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DbApplicationContext _dbApplicationContext;

        public AdminController(IUnitOfWork unitOfWork,
            IMapper mapper, DbApplicationContext dbApplicationContext )
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
            _dbApplicationContext=dbApplicationContext;
        }

        public async Task<IActionResult> Index()
        {

            var movies =await _unitOfWork.movieRepo.GetAllAsync();
            if (movies == null)
                return NotFound();
            else
                return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddMovieVM();

            // تحميل الأنواع من قاعدة البيانات بشكل غير متزامن
            var genres = await _unitOfWork.genreRepo.GetAllAsync();

            model.GenreList = genres.Select(g => new SelectListItem
            {
                Text = g.Name,  // اسم النوع
                Value = g.Id.ToString()  // قيمة النوع
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieVM model)
        {
            if (ModelState.IsValid)
            {
                // إنشاء كائن الفيلم وإضافته إلى قاعدة البيانات

                var movie = _mapper.Map<AddMovieVM, Movie>(model);

                _unitOfWork.movieRepo.Add(movie);   // إضافة الفيلم أولًا

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

                   await _unitOfWork.Complete(); // حفظ العلاقات في قاعدة البيانات
                }

                return RedirectToAction("Index");  // إعادة التوجيه إلى قائمة الأفلام
            }
            //// إعادة تحميل قائمة الأنواع عند حدوث خطأ
            //model.GenreList = _unitOfWork.genreRepo.GetAllAsync()
            //    .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
            //    .ToList();
            var genres = await _unitOfWork.genreRepo.GetAllAsync();

            model.GenreList = genres.Select(g => new SelectListItem
            {
                Text = g.Name,  // اسم النوع
                Value = g.Id.ToString()  // قيمة النوع
            }).ToList();


            return View(model);  // إذا كان هناك خطأ في النموذج، نعرضه مرة أخرى
        }


        public async Task<IActionResult> Details(int id, string name = "Details")
        {
            var entity =await _unitOfWork.movieRepo.GetMoviesWithGenresAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var detailsMovie = _mapper.Map<Movie, DetailsEditMovieVM>(entity);

            // ✅ جلب جميع الأنواع المتاحة وتحويلها إلى SelectListItem
            //detailsMovie.GenreList = _unitOfWork.genreRepo.GetAll()
            //    .Select(g => new SelectListItem
            //    {
            //        Value = g.Id.ToString(),
            //        Text = g.Name,
            //        Selected = entity.MovieGenres.Any(mg => mg.GenreId == g.Id) // تحديد الأنواع المختارة مسبقًا
            //    })
            //    .ToList();
            var genres = await _unitOfWork.genreRepo.GetAllAsync();

            detailsMovie.GenreList = genres.Select(g => new SelectListItem
            {
                Text = g.Name,  // اسم النوع
                Value = g.Id.ToString(),
                Selected = entity.MovieGenres.Any(mg => mg.GenreId == g.Id)// قيمة النوع
            }).ToList();


            return View(name, detailsMovie);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DetailsEditMovieVM entity)
        {
            if (!ModelState.IsValid)
            {
                #region \\
                 //// If the model is invalid, re-populate the genres list and return to the view.
                //entity.GenreList = await _unitOfWork.genreRepo.GetAllAsync()
                //    .Select(g => new SelectListItem
                //    {
                //        Value = g.Id.ToString(),
                //        Text = g.Name,
                //        Selected = entity.SelectedGenres != null && entity.SelectedGenres.Contains(g.Id)
                //    })
                //    .ToList();
                #endregion
               
                var genres = await _unitOfWork.genreRepo.GetAllAsync();

                entity.GenreList = genres.Select(g => new SelectListItem
                {
                    Text = g.Name,  // اسم النوع
                    Value = g.Id.ToString(),  // قيمة النوع
                    Selected = entity.MovieGenres.Any(mg => mg.GenreId == g.Id)
                }).ToList();

                return View(entity);
            }

            var movie = await _unitOfWork.movieRepo.GetMoviesWithGenresAsync(entity.Id);
            if (movie == null)
            {
                return NotFound(); // Movie not found
            }

            // Update the main movie details

            // If the user has selected genres, update MovieGenres
            if (entity.SelectedGenres != null && entity.SelectedGenres.Any())
            {
                // First, remove the old genres associated with the movie
                _dbApplicationContext.MovieGenre.RemoveRange(movie.MovieGenres);

                // Then, add the new selected genres to the collection
                foreach (var genreId in entity.SelectedGenres)
                {
                    movie.MovieGenres.Add(new MovieGenre { MovieId = movie.Id, GenreId = genreId });
                }
            }

            // Save changes to the database
            _unitOfWork.movieRepo.Update(movie);
           await _unitOfWork.Complete();
            return RedirectToAction("Index"); // Redirect to the index page after the update
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _unitOfWork.movieRepo.GetAsync(id); // Fetch movie first

            if (movie == null)
            {
                return NotFound(); // Movie doesn't exist, return 404
            }

            _unitOfWork.movieRepo.Delete(movie); // Delete only if found
          await  _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

    }
}