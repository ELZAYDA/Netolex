using AutoMapper;
using BLL.Repositiries.Implementation;
using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.GenreVM;
using Netolewx.ViewModels.MovieVM.MovieVM;

namespace Netolewx.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepo _genrerepo;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepo genrerepo,IMapper mapper)
        {
            _genrerepo = genrerepo;
            _mapper=mapper;
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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddGenreVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                var genre = _mapper.Map<AddGenreVM, Genre>(entity);

                _genrerepo.Add(genre);
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult Details(int id,string name= "Details")
        {
            var entity = _genrerepo.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            var detailsGenre = _mapper.Map<Genre, DetailsEditGenreVM>(entity);


            return View(name, detailsGenre);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit(DetailsEditGenreVM entity)
        {
            if (ModelState.IsValid)
            {
                var genre = _genrerepo.Get(entity.Id);

                if (genre == null)
                {
                    return NotFound();
                }

            genre = _mapper.Map<DetailsEditGenreVM,Genre>(entity);

                _genrerepo.Update(genre);
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult Delete(int id)
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
