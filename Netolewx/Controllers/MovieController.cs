using AutoMapper;
using BLL.Repositiries.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Netolewx.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var movie = _unitOfWork.movieRepo.GetMoviesWithGenres(id);
            if(movie != null) 
                return View(movie);
            return NotFound();
        }
    }
}
