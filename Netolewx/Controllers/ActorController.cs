using AutoMapper;
using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.ActorVM;
using Netolewx.ViewModels.MovieVM.MovieVM;

namespace Netolewx.Controllers
{
    public class ActorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public IActionResult Index()
        {
            var actors = _unitOfWork.actorRepo.GetAll();
            if (actors == null)
                return NotFound();
            return View(actors);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddActorVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                var actor = _mapper.Map<AddActorVM, Actor>(entity);
                _unitOfWork.actorRepo.Add(actor);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult Details(int id, string name = "Details")
        {
            var entity = _unitOfWork.actorRepo.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            var detailsActor = _mapper.Map<Actor, DetailsEditActorVM>(entity);
            return View(name, detailsActor);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit(DetailsEditActorVM entity)
        {
            if (ModelState.IsValid)
            {
                var actor = _unitOfWork.actorRepo.Get(entity.Id);

                if (actor == null)
                {
                    return NotFound();
                }

                actor = _mapper.Map<DetailsEditActorVM, Actor>(entity);


                _unitOfWork.actorRepo.Update(actor);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var actor = _unitOfWork.actorRepo.Get(id);

            if (actor == null)
            {
                return NotFound();
            }

            _unitOfWork.actorRepo.Delete(actor);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
