using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Netolewx.Models;
using System.Diagnostics;

namespace Netolewx.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly DbApplicationContext _dbcontext;
        private readonly IMovieRepo _movierepo;

        public HomeController(ILogger<HomeController> logger,
            DbApplicationContext dbcontext,
            IMovieRepo movierepo)
        {
            _logger = logger;
            _dbcontext=dbcontext;
            _movierepo=movierepo;
        }

        public IActionResult Index()
        {
            var movies = _movierepo.GetAll();
            if(movies == null)
                return NotFound();
            else
            return View(movies);
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
