using AutoMapper;
using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.ReviewVM;

namespace Netolewx.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public IActionResult Index()
        {
            var reviews = _unitOfWork.reviewRepo.GetAll();
            if (reviews == null)
                return NotFound();
            return View(reviews);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddReviewVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                var review = _mapper.Map<AddReviewVM,Review>(entity);
                _unitOfWork.reviewRepo.Add(review);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult Details(int id, string name = "DetailsReview")
        {
            var entity = _unitOfWork.reviewRepo.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            var detailsReview=_mapper.Map<Review,DetailsEditReviewVM>(entity);  

            return View(name, detailsReview);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit(DetailsEditReviewVM entity)
        {
            if (ModelState.IsValid)
            {
                var review = _unitOfWork.reviewRepo.Get(entity.Id);

                if (review == null)
                {
                    return NotFound();
                }

                review = _mapper.Map<DetailsEditReviewVM, Review>(entity);
                _unitOfWork.reviewRepo.Update(review);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var review = _unitOfWork.reviewRepo.Get(id);

            if (review == null)
            {
                return NotFound();
            }

            _unitOfWork.reviewRepo.Delete(review);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
