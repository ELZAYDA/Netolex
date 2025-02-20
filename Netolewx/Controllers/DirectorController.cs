using AutoMapper;
using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.DirectorVM;
using Netolewx.ViewModels.MovieVM.MovieVM;
using Netolex.Services.Document_Settings;

namespace Netolewx.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DirectorController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {
            var directors =await _unitOfWork.directorRepo.GetAllAsync();
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
        public async Task<IActionResult> Add(AddDirectorVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                if (entity.ImageName!=null)
                    entity.Image =await DocumentSettings.UploadFile(entity.ImageName, "images");
                else
                    entity.Image="No image";

                var director = _mapper.Map<AddDirectorVM, Director>(entity);

                _unitOfWork.directorRepo.Add(director);
                await _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public async Task<IActionResult> Details(int id, string name = "Details")
        {
            var entity = await _unitOfWork.directorRepo.GetAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var detailsDirector = _mapper.Map<Director, DetailsEditDirectorVM>(entity);
            TempData["ImageName"]=detailsDirector.ImageName;

            return View(name, detailsDirector);
        }

        [HttpGet]
        public Task<IActionResult> Edit(int id)
        {
            

            return Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DetailsEditDirectorVM entity)
        {

            if (ModelState.IsValid)
            {
                if (entity.Image == null)
                {
                    entity.Image=await DocumentSettings.UploadFile(entity.ImageName, "images");
                }
                var director =await _unitOfWork.directorRepo.GetAsync(entity.Id);

                if (director == null)
                {
                    return NotFound();
                }

                director = _mapper.Map<DetailsEditDirectorVM, Director>(entity);

                _unitOfWork.directorRepo.Update(director);
                await _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var director = await _unitOfWork.directorRepo.GetAsync(id);

            if (director == null)
            {
                return NotFound();
            }

            _unitOfWork.directorRepo.Delete(director);
            var count =await _unitOfWork.Complete();
            if (count > 0)
                DocumentSettings.DeleteFile(director.Image, "images");

            return RedirectToAction("Index");
        }
    }
}
