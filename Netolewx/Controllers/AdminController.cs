using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly DbApplicationContext _dbApplicationContext;

        public AdminController(
            IMovieRepo movierepo, IMapper mapper,DbApplicationContext dbApplicationContext)
        {
            _movierepo=movierepo;
            _mapper=mapper;
            _dbApplicationContext=dbApplicationContext;
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
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddMovieVM entity)
        {
            if (entity != null)
                if (ModelState.IsValid)
                {
                    var movie = _mapper.Map<AddMovieVM, Movie>(entity);
                    _movierepo.Add(movie);

                    return RedirectToAction("Index");
                }
            return View(entity);
        }

        public IActionResult Details(int id, string name = "Details")
        {
            var entity = _movierepo.Get(id);

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
                var movie = _movierepo.Get(entity.Id); // Fetch existing movie from DB

                if (movie == null)
                {
                    return NotFound(); // Handle movie not found
                }

                movie = _mapper.Map<DetailsEditMovieVM, Movie>(entity);
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
