using BLL.Repositiries.Implementation;
using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.GenreVM;

namespace Netolewx.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepo _genrerepo;

        public GenreController(IGenreRepo genrerepo)
        {
            _genrerepo = genrerepo;
        }

        public IActionResult Index()
        {
            var genres = _genrerepo.GetAll();
            if (genres == null)
                return NotFound();
            else
                return View(genres);
        }

        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(AddGenreVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                var genre = new Genre
                {
                    Name = entity.Name,
                    Description = entity.Description,
                };
                _genrerepo.Add(genre);
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult DetailsGenre(int id,string name= "DetailsGenre")
        {
            var entity = _genrerepo.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            var detailsGenre = new DetailsEditGenreVM
            {
                id = id,
                Name = entity.Name,
                Description = entity.Description,
            };

            return View(name, detailsGenre);
        }

        [HttpGet]
        public IActionResult EditGenre(int id)
        {
            return DetailsGenre(id, "EditGenre");
        }

        [HttpPost]
        public IActionResult EditGenre(DetailsEditGenreVM entity)
        {
            if (ModelState.IsValid)
            {
                var genre = _genrerepo.Get(entity.id);

                if (genre == null)
                {
                    return NotFound();
                }

                genre.Name = entity.Name;
                genre.Description = entity.Description;

                _genrerepo.Update(genre);
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult DeleteGenre(int id)
        {
            var genre = _genrerepo.Get(id);

            if (genre == null)
            {
                return NotFound();
            }

            _genrerepo.Delete(genre);
            return RedirectToAction("Index");
        }
    }
}
