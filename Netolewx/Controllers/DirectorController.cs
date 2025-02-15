using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.DirectorVM;

namespace Netolewx.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IDirectorRepo _directorRepo;

        public DirectorController(IDirectorRepo directorRepo)
        {
            _directorRepo = directorRepo;
        }

        public IActionResult Index()
        {
            var directors = _directorRepo.GetAll();
            if (directors == null)
                return NotFound();
            return View(directors);
        }

        [HttpGet]
        public IActionResult AddDirector()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDirector(AddDirectorVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                var director = new Director
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    DateOfBirth = entity.DateOfBirth,
                    Biography = entity.Biography,
                    ProfilePictureUrl = entity.ProfilePictureUrl
                };
                _directorRepo.Add(director);
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult DetailsDirector(int id, string name = "DetailsDirector")
        {
            var entity = _directorRepo.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            var detailsDirector = new DetailsEditDirectorVM
            {
                Id = id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                Biography = entity.Biography,
                ProfilePictureUrl = entity.ProfilePictureUrl
            };

            return View(name, detailsDirector);
        }

        [HttpGet]
        public IActionResult EditDirector(int id)
        {
            return DetailsDirector(id, "EditDirector");
        }

        [HttpPost]
        public IActionResult EditDirector(DetailsEditDirectorVM entity)
        {
            if (ModelState.IsValid)
            {
                var director = _directorRepo.Get(entity.Id);

                if (director == null)
                {
                    return NotFound();
                }

                director.FirstName = entity.FirstName;
                director.LastName = entity.LastName;
                director.DateOfBirth = entity.DateOfBirth;
                director.Biography = entity.Biography;
                director.ProfilePictureUrl = entity.ProfilePictureUrl;

                _directorRepo.Update(director);
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult DeleteDirector(int id)
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
