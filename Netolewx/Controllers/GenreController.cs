using AutoMapper;
using BLL.Repositiries.Implementation;
using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.GenreVM;
using Netolewx.ViewModels.MovieVM.MovieVM;
using System.Threading.Tasks;

namespace Netolewx.Controllers
{
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _unitOfWork.genreRepo.GetAllAsync();  // Async call to get all genres
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
        public async Task<IActionResult> Add(AddGenreVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                var genre = _mapper.Map<AddGenreVM, Genre>(entity);

                 _unitOfWork.genreRepo.Add(genre);  // Async call to add genre
                await _unitOfWork.Complete();  // Async call to complete the unit of work
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public async Task<IActionResult> Details(int id, string name = "Details")
        {
            var entity = await _unitOfWork.genreRepo.GetAsync(id);  // Async call to get genre by id

            if (entity == null)
            {
                return NotFound();
            }

            var detailsGenre = _mapper.Map<Genre, DetailsEditGenreVM>(entity);

            return View(name, detailsGenre);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id, "Edit");  // Reuse Details method for editing
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DetailsEditGenreVM entity)
        {
            if (ModelState.IsValid)
            {
                var genre = await _unitOfWork.genreRepo.GetAsync(entity.Id);  // Async call to get genre by id

                if (genre == null)
                {
                    return NotFound();
                }

                genre = _mapper.Map<DetailsEditGenreVM, Genre>(entity);

                 _unitOfWork.genreRepo.Update(genre);  // Async call to update genre
                await _unitOfWork.Complete();  // Async call to complete the unit of work
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _unitOfWork.genreRepo.GetAsync(id);  // Async call to get genre by id

            if (genre == null)
            {
                return NotFound();
            }

             _unitOfWork.genreRepo.Delete(genre);  // Async call to delete genre
            await _unitOfWork.Complete();  // Async call to complete the unit of work
            return RedirectToAction("Index");
        }
    }
}
