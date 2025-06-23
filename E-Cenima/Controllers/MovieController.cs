using BLL.Services.Movies;
using Microsoft.AspNetCore.Mvc;

namespace E_Cenima.Controllers
{
    public class MovieController(IMovieService _movieService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllAsync();
            return View(movies);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            var movieDetails = await _movieService.GetByIdAsync(id.Value);
            if (movieDetails == null)
            {
                return NotFound();
            }
            return View(movieDetails);
        }
    }
}
