using AutoMapper;
using BLL.Repositiries.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Netolewx.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _unitOfWork.movieRepo.GetMoviesWithGenresAsync(id); // Async call
            if (movie != null)
                return View(movie);
            return NotFound();
        }
    }
}
