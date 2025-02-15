using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.ViewModels.ReviewVM;

namespace Netolewx.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepo _reviewRepo;

        public ReviewController(IReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        public IActionResult Index()
        {
            var reviews = _reviewRepo.GetAll();
            if (reviews == null)
                return NotFound();
            return View(reviews);
        }

        [HttpGet]
        public IActionResult AddReview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddReview(AddReviewVM entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                var review = new Review
                {
                    Rating = entity.Rating,
                    Comment = entity.Comment,
                    CreatedAt = DateTime.Now,
                };
                _reviewRepo.Add(review);
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        public IActionResult DetailsReview(int id, string name = "DetailsReview")
        {
            var entity = _reviewRepo.Get(id);

            if (entity == null)
            {
                return NotFound();
            }

            var detailsReview = new DetailsEditReviewVM
            {
                Id = id,
                Rating = entity.Rating,
                Comment = entity.Comment,
            };

            return View(name, detailsReview);
        }

        [HttpGet]
        public IActionResult EditReview(int id)
        {
            return DetailsReview(id, "EditReview");
        }

        [HttpPost]
        public IActionResult EditReview(DetailsEditReviewVM entity)
        {
            if (ModelState.IsValid)
            {
                var review = _reviewRepo.Get(entity.Id);

                if (review == null)
                {
                    return NotFound();
                }

                review.Rating = entity.Rating;
                review.Comment = entity.Comment;

                _reviewRepo.Update(review);
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        public IActionResult DeleteReview(int id)
        {
            var review = _reviewRepo.Get(id);

            if (review == null)
            {
                return NotFound();
            }

            _reviewRepo.Delete(review);
            return RedirectToAction("Index");
        }
    }
}
