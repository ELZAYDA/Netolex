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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public IActionResult Index()
        {
            var genres = _unitOfWork.genreRepo.GetAll();
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

                _unitOfWork.genreRepo.Add(genre);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult Details(int id,string name= "Details")
        {
            var entity = _unitOfWork.genreRepo.Get(id);

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
                var genre = _unitOfWork.genreRepo.Get(entity.Id);

                if (genre == null)
                {
                    return NotFound();
                }

            genre = _mapper.Map<DetailsEditGenreVM,Genre>(entity);

                _unitOfWork.genreRepo.Update(genre);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var genre = _unitOfWork.genreRepo.Get(id);

            if (genre == null)
            {
                return NotFound();
            }

            _unitOfWork.genreRepo.Delete(genre);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
