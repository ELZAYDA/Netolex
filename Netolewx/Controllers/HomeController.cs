using AutoMapper;
using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Netolewx.Models;
using Netolex.ViewModels.MovieVM;
using System.Diagnostics;

namespace Netolewx.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbApplicationContext _dbcontext;
        private readonly IMapper _mapper;


        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork,
            DbApplicationContext dbcontext,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork=unitOfWork;
            _dbcontext=dbcontext;
            _mapper=mapper;
        }

        public IActionResult Index(string search, string genreFilter)
        {
            var Movies = _unitOfWork.movieRepo.GetMoviesWithGenres(); // جلب جميع الأفلام

            if (!string.IsNullOrEmpty(search))
            {
                Movies = Movies.Where(m => m.Title.ToLower().Contains(search.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(genreFilter))
            {
                Movies = Movies.Where(m => m.MovieGenres.Any(mg => mg.Genre.Name == genreFilter)).ToList();
            }

            var genres = _unitOfWork.genreRepo.GetAll(); // جلب جميع الأنواع
            var viewModel = new MovieVM
            {
                Movies = Movies.ToList(),
                Genres = genres.ToList()
            };

            return View(viewModel);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
