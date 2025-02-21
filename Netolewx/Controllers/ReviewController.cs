//using AutoMapper;
//using BLL.Repositiries.Interfaces;
//using DAL.Models;
//using Microsoft.AspNetCore.Mvc;
//using Netolewx.ViewModels.ReviewVM;
//using System.Threading.Tasks;

//namespace Netolewx.Controllers
//{
//    public class ReviewController : Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;

//        public ReviewController(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }

//        // Asynchronous Index Action
//        public async Task<IActionResult> Index()
//        {
//            var reviews = await _unitOfWork.reviewRepo.GetAllAsync();  // Async call
//            if (reviews == null)
//                return NotFound();
//            return View(reviews);
//        }

//        // Asynchronous Add Action (GET)
//        [HttpGet]
//        public IActionResult Add()
//        {
//            return View();
//        }

//        // Asynchronous Add Action (POST)
//        [HttpPost]
//        public async Task<IActionResult> Add(AddReviewVM entity)
//        {
//            if (entity != null && ModelState.IsValid)
//            {
//                var review = _mapper.Map<AddReviewVM, Review>(entity);
//                 _unitOfWork.reviewRepo.Add(review); // Async call
//                await _unitOfWork.Complete(); // Async call
//                return RedirectToAction("Index");
//            }
//            return View(entity);
//        }

//        // Asynchronous Details Action
//        public async Task<IActionResult> Details(int id, string name = "DetailsReview")
//        {
//            var entity = await _unitOfWork.reviewRepo.GetAsync(id);  // Async call

//            if (entity == null)
//            {
//                return NotFound();
//            }

//            var detailsReview = _mapper.Map<Review, DetailsEditReviewVM>(entity);
//            return View(name, detailsReview);
//        }

//        // Asynchronous Edit Action (GET)
//        [HttpGet]
//        public async Task<IActionResult> Edit(int id)
//        {
//            return await Details(id, "Edit");
//        }

//        // Asynchronous Edit Action (POST)
//        [HttpPost]
//        public async Task<IActionResult> Edit(DetailsEditReviewVM entity)
//        {
//            if (ModelState.IsValid)
//            {
//                var review = await _unitOfWork.reviewRepo.GetAsync(entity.Id); // Async call

//                if (review == null)
//                {
//                    return NotFound();
//                }

//                review = _mapper.Map<DetailsEditReviewVM, Review>(entity);
//                 _unitOfWork.reviewRepo.Update(review); // Async call
//                await _unitOfWork.Complete(); // Async call
//                return RedirectToAction("Index");
//            }

//            return View(entity);
//        }

//        // Asynchronous Delete Action
//        [HttpPost]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var review = await _unitOfWork.reviewRepo.GetAsync(id); // Async call

//            if (review == null)
//            {
//                return NotFound();
//            }

//             _unitOfWork.reviewRepo.Delete(review); // Async call
//            await _unitOfWork.Complete(); // Async call
//            return RedirectToAction("Index");
//        }
//    }
//}
