using BLL.DTO.Movie;
using BLL.Factory.MovieFactory;
using BLL.Services.ActorService;
using BLL.Services.Movies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Cenima.Controllers
{
    public class MovieController(IMovieService _movieService, IActorService _actorService) : Controller
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


        //admin Controller

        public async Task<IActionResult> AdminIndex()
        {
            var movies = await _movieService.GetAllAdminAsync();


            return  View("AdminIndex",movies);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var movie = await _movieService.GetByIdAsync(Id);

            var movieEdit = new MovieEditDto
            {
                Id=movie.Id,
                Name=movie.Name,
                Description = movie.Description,
                ImageURL = movie.ImageURL,
                ReleaseDate = movie.ReleaseDate,
                TrailerURL = movie.TrailerURL,
                MovieCategory = movie.MovieCategory,
            };
            return View(movieEdit);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MovieAddDto movieAddDto)
        {

            try
            {
            await _movieService.CreateAsync(movieAddDto);

            }catch(Exception ex)
            {
                return await AdminIndex();

            }

            return await AdminIndex();
        }
    }
}
