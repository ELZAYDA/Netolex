using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netolewx.ViewModels;
using Netolewx.ViewModels.MovieVM;
using Netolewx.ViewModels.MovieVM.MovieVM;

namespace Netolewx.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMovieRepo _movierepo;

        public AdminController(
            IMovieRepo movierepo)
        {
            _movierepo=movierepo;
        }
        public IActionResult MoviesManagement()
        {

            var movies = _movierepo.GetAll();
            if (movies == null)
                return NotFound();
            else
                return View(movies);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(AddMovieVM entity)
        {
            if (entity != null)
                if (ModelState.IsValid)
                {
                    var movie = new Movie
                    {
                        Title = entity.Title,
                        Description = entity.Description,
                        ReleaseYear = entity.ReleaseYear,
                        Duration = entity.Duration,
                        PosterUrl = entity.PosterUrl,
                        TrailerUrl = entity.TrailerUrl,
                        CreatedAt = DateTime.Now, // تعيين التاريخ الحالي
                    };
                    _movierepo.Add(movie);
                    return RedirectToAction("MoviesManagement");
                }
            return View(entity);
        }

        public IActionResult DetailsMovie(int id, string name = "DetailsMovie")
        {
            var entity = _movierepo.Get(id);

            if (entity == null) // Check if the movie exists first
            {
                return NotFound(); // Return 404 if movie doesn't exist
            }

            var detailsMovie = new DetailsEditMovieVM
            {
                id = id,
                Title = entity.Title,
                Description = entity.Description,
                ReleaseYear = entity.ReleaseYear,
                Duration = entity.Duration,
                PosterUrl = entity.PosterUrl,
                TrailerUrl = entity.TrailerUrl,
                CreatedAt = entity.CreatedAt // Get the actual creation time from DB if available
            };

            return View(name, detailsMovie);
        }

        [HttpGet]
        public IActionResult EditMovie(int id)
        {
            return DetailsMovie(id, "EditMovie");
        }

        [HttpPost]
        public IActionResult EditMovie(DetailsEditMovieVM entity)
        {
            if (ModelState.IsValid)
            {
                var movie = _movierepo.Get(entity.id); // Fetch existing movie from DB

                if (movie == null)
                {
                    return NotFound(); // Handle movie not found
                }

                // Update movie properties
                movie.Title = entity.Title;
                movie.Description = entity.Description;
                movie.ReleaseYear = entity.ReleaseYear;
                movie.Duration = entity.Duration;
                movie.PosterUrl = entity.PosterUrl;
                movie.TrailerUrl = entity.TrailerUrl;
                movie.UpdatedAt = DateTime.Now; // Use UpdatedAt instead of CreatedAt

                _movierepo.Update(movie); // Save updates to DB
                return RedirectToAction("MoviesManagement");
            }

            return View(entity); // Return form with validation errors
        }

        [HttpPost]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _movierepo.Get(id); // Fetch movie first

            if (movie == null)
            {
                return NotFound(); // Movie doesn't exist, return 404
            }

            _movierepo.Delete(movie); // Delete only if found

            return RedirectToAction("MoviesManagement");
        }

    }
}
