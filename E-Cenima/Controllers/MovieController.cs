using BLL.DTO.Movie;
using BLL.Services.ActorService;
using BLL.Services.Movies;
using Microsoft.AspNetCore.Mvc;

namespace E_Cenima.Controllers
{
    public class MovieController(IMovieService _movieService ,IActorService _actorService) : Controller
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
        public async Task<IActionResult> MoviesByActor(int id)
        {
            var Movies = await _movieService.GetMovieByActor(id);
            var actor = await _actorService.GetByIdAsync(id);
            MoviesbyActorDto moviesbyActor = new MoviesbyActorDto
            {
                Actor = actor,
                Movies = Movies
            };
            return View(moviesbyActor);
        }
        public async Task<IActionResult> MoviesWithTime(int Id)
        {
            var MovieWithTime = await _movieService.GetMoviesWithShowtimesAsync(Id);
            //Console.WriteLine(MovieWithTime) ;
            return View(MovieWithTime);
        }
    }
}
