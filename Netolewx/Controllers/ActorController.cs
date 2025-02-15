using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.ActorVM;

namespace Netolewx.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorRepo _actorRepo;

        public ActorController(IActorRepo actorRepo)
        {
            _actorRepo = actorRepo;
        }

        public IActionResult Index()
        {
            var actors = _actorRepo.GetAll();
            if (actors == null)
                return NotFound();
            return View(actors);
        }

        [HttpGet]
        public IActionResult AddActor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddActor(AddActorVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                var actor = new Actor
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    DateOfBirth = entity.DateOfBirth,
                    Biography = entity.Biography,
                    ProfilePictureUrl = entity.ProfilePictureUrl
                };
                _actorRepo.Add(actor);
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult DetailsActor(int id, string name = "DetailsActor")
        {
            var entity = _actorRepo.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            var detailsActor = new DetailsEditActorVM
            {
                Id = id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                Biography = entity.Biography,
                ProfilePictureUrl = entity.ProfilePictureUrl
            };

            return View(name, detailsActor);
        }

        [HttpGet]
        public IActionResult EditActor(int id)
        {
            return DetailsActor(id, "EditActor");
        }

        [HttpPost]
        public IActionResult EditActor(DetailsEditActorVM entity)
        {
            if (ModelState.IsValid)
            {
                var actor = _actorRepo.Get(entity.Id);

                if (actor == null)
                {
                    return NotFound();
                }

                actor.FirstName = entity.FirstName;
                actor.LastName = entity.LastName;
                actor.DateOfBirth = entity.DateOfBirth;
                actor.Biography = entity.Biography;
                actor.ProfilePictureUrl = entity.ProfilePictureUrl;

                _actorRepo.Update(actor);
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult DeleteActor(int id)
        {
            var actor = _actorRepo.Get(id);

            if (actor == null)
            {
                return NotFound();
            }

            _actorRepo.Delete(actor);
            return RedirectToAction("Index");
        }
    }
}
