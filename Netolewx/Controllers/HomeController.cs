using AutoMapper;
using BLL.Repositiries.Interfaces;
using DAL.Contexts;
using DAL.Models;
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
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
            DbApplicationContext dbcontext,
            IMovieRepo movierepo,
            IMapper mapper)
        {
            _logger = logger;
            _dbcontext=dbcontext;
            _movierepo=movierepo;
            _mapper=mapper;
        }

        public IActionResult Index(string search)
        {
            var Movies = Enumerable.Empty<Movie>();
            if (string.IsNullOrEmpty(search))
            {
                Movies = _movierepo.GetAll();
                if (Movies == null)
                    return NotFound();
            }
            else
            {
                Movies = _movierepo.GetByName(search.ToLower());
            }
            return View(Movies);

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
