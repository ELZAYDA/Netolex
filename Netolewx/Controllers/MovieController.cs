using AutoMapper;
using BLL.Repositiries.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Netolewx.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepo _movierepo;
        private readonly IMapper _mapper;

        public MovieController(
            IMovieRepo movierepo,
            IMapper mapper)
        {
            _movierepo=movierepo;
            _mapper=mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var movie = _movierepo.GetMoviesWithGenres(id);
            if(movie != null) 
                return View(movie);
            return NotFound();
        }
    }
}
