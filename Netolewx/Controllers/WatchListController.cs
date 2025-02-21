using BLL.Repositiries.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Netolex.ViewModels.MovieVM;

namespace Netolex.Controllers
{
    [Authorize]
    public class WatchListController :Controller
    {

        private readonly IWatchListRepo _watchlistService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public WatchListController(
            IWatchListRepo watchlistService,
            UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork)
        {
            _watchlistService = watchlistService;
            _userManager = userManager;
            _unitOfWork=unitOfWork;
        }

        // GET: /Home/Watchlist
        
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var watchlistMovies = await _watchlistService.GetUserWatchlistAsync(userId);

            // Set watchlist count for navbar
            ViewBag.WatchListCount = watchlistMovies.Count;

            return View("watchlistMovies", watchlistMovies); 
           
        }


        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(int movieId)
        {
         
            var userId = _userManager.GetUserId(User);
            var result = await _watchlistService.AddToWatchlistAsync(userId, movieId);
            
            if (result)
            {
                // Redirect to the Watchlist page after adding the movie
                return RedirectToAction("Index","Home");
            }
            
            // If adding fails, show an error page or return to the previous page with an error message
            TempData["ErrorMessage"] = "Failed to add movie to watchlist.";
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchlist(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _watchlistService.RemoveFromWatchlistAsync(userId, movieId);

            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Remove movie to watchlist.";
                return RedirectToAction("Index", "Home");
            }
        }

       
        [HttpGet]
        public async Task<IActionResult> IsInWatchlist(int movieId)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _watchlistService.IsInWatchlistAsync(userId, movieId);

            return Json(new { isInWatchlist = result });
        }
    }
}
