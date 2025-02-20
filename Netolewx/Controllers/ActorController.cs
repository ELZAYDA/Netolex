using AutoMapper;
using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.ActorVM;
using Netolewx.ViewModels.MovieVM.MovieVM;
using Netolex.Services.Document_Settings;

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

        public async Task<IActionResult> Index()
        {
            var actors = await _unitOfWork.actorRepo.GetAllAsync(); // Await the asynchronous call
            if (actors == null || !actors.Any()) // Check if the result is null or empty
                return NotFound();

            return View(actors);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddActorVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                if (entity.ImageName!=null)
                    entity.Image =await DocumentSettings.UploadFile(entity.ImageName, "images");
                else
                    entity.Image="No image";
                var actor = _mapper.Map<AddActorVM, Actor>(entity);
                _unitOfWork.actorRepo.Add(actor);
               var count = await _unitOfWork.Complete();

               return RedirectToAction("Index");
            }
            return View(entity);
        }

        public async Task<IActionResult> Details(int id, string name = "Details")
        {
            var entity = await _unitOfWork.actorRepo.GetAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var detailsActor = _mapper.Map<Actor, DetailsEditActorVM>(entity);
            return View(name, detailsActor);
        }

        [HttpGet]
        public Task<IActionResult> Edit(int id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult >Edit(DetailsEditActorVM entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.Image == null)
                {
                    entity.Image=await DocumentSettings.UploadFile(entity.ImageName, "images");
                }
                var actor = await _unitOfWork.actorRepo.GetAsync(entity.Id);

                if (actor == null)
                {
                    return NotFound();
                }

                actor = _mapper.Map<DetailsEditActorVM, Actor>(entity);


                _unitOfWork.actorRepo.Update(actor);
                await _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _unitOfWork.actorRepo.GetAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _unitOfWork.actorRepo.Delete(actor);
            var count =await _unitOfWork.Complete();
            if (count > 0)
                 DocumentSettings.DeleteFile(actor.Image, "images");
            return RedirectToAction("Index");
        }
    }
}
