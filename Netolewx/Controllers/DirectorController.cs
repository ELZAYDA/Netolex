using AutoMapper;
using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.DirectorVM;
using Netolewx.ViewModels.MovieVM.MovieVM;

namespace Netolewx.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IDirectorRepo _directorRepo;
        private readonly IMapper _mapper;

        public DirectorController(IDirectorRepo directorRepo,IMapper mapper)
        {
            _directorRepo = directorRepo;
            _mapper=mapper;
        }

        public IActionResult Index()
        {
            var directors = _directorRepo.GetAll();
            if (directors == null)
                return NotFound();
            return View(directors);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddDirectorVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                var director = _mapper.Map<AddDirectorVM, Director>(entity);

                _directorRepo.Add(director);
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult Details(int id, string name = "Details")
        {
            var entity = _directorRepo.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            var detailsDirector = _mapper.Map<Director, DetailsEditDirectorVM>(entity);


            return View(name, detailsDirector);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit(DetailsEditDirectorVM entity)
        {
            if (ModelState.IsValid)
            {
                var director = _directorRepo.Get(entity.Id);

                if (director == null)
                {
                    return NotFound();
                }

                director = _mapper.Map<DetailsEditDirectorVM, Director>(entity);

                _directorRepo.Update(director);
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var director = _directorRepo.Get(id);

            if (director == null)
            {
                return NotFound();
            }

            _directorRepo.Delete(director);
            return RedirectToAction("Index");
        }
    }
}
