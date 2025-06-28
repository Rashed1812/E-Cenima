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
            return View(MovieWithTime);
        }

        // Admin Controller Actions

        public async Task<IActionResult> AdminIndex()
        {
            try
            {
                var movies = await _movieService.GetAllAdminAsync();
                return View("AdminIndex", movies);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading movies: " + ex.Message;
                return View("AdminIndex", new List<object>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                var movie = await _movieService.GetByIdAsync(Id);
                if (movie == null)
                {
                    TempData["ErrorMessage"] = "Movie not found.";
                    return RedirectToAction("AdminIndex");
                }

                var movieEdit = new MovieEditDto
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Description = movie.Description,
                    ImageURL = movie.ImageURL,
                    ReleaseDate = movie.ReleaseDate,
                    TrailerURL = movie.TrailerURL,
                    MovieCategory = movie.MovieCategory,
                };
                return View(movieEdit);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading movie for edit: " + ex.Message;
                return RedirectToAction("AdminIndex");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int? Id, MovieEditDto movieEditDto)
        {
            if (Id == null || Id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid movie ID.";
                return RedirectToAction("AdminIndex");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill all required fields correctly.";
                return View(movieEditDto);
            }

            try
            {
                var updateMovie = new UpdateMovieDTO
                {
                    Id = movieEditDto.Id,
                    Name = movieEditDto.Name,
                    Description = movieEditDto.Description,
                    ImageURL = movieEditDto.ImageFile,
                    ReleaseDate = movieEditDto.ReleaseDate,
                    TrailerURL = movieEditDto.TrailerURL,
                    MovieCategory = movieEditDto.MovieCategory
                };
                 _movieService.UpdateAsync(updateMovie);
                TempData["SuccessMessage"] = "Movie updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error updating movie: " + ex.Message;
                return View(movieEditDto);
            }
            return RedirectToAction("AdminIndex");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                // Get actors list for the dropdown
                var actors = await _actorService.GetAllAsync();
                ViewBag.ActorsList = actors.Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                }).ToList();
            }
            catch (Exception ex)
            {
                // If there's an error getting actors, set empty list
                ViewBag.ActorsList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                TempData["WarningMessage"] = "Could not load actors list: " + ex.Message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MovieAddDto movieAddDto)
        {
            if (!ModelState.IsValid)
            {
                // Reload actors list for the view
                try
                {
                    var actors = await _actorService.GetAllAsync();
                    ViewBag.ActorsList = actors.Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.FullName
                    }).ToList();
                }
                catch (Exception ex)
                {
                    ViewBag.ActorsList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                    TempData["WarningMessage"] = "Could not load actors list: " + ex.Message;
                }

                TempData["ErrorMessage"] = "Please fill all required fields correctly.";
                return View(movieAddDto);
            }

            try
            {
                await _movieService.CreateAsync(movieAddDto);
                TempData["SuccessMessage"] = "Movie added successfully!";
            }
            catch (Exception ex)
            {
                // Reload actors list for the view in case of error
                try
                {
                    var actors = await _actorService.GetAllAsync();
                    ViewBag.ActorsList = actors.Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.FullName
                    }).ToList();
                }
                catch
                {
                    ViewBag.ActorsList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                }

                TempData["ErrorMessage"] = "Error adding movie: " + ex.Message;
                return View(movieAddDto);
            }
            return RedirectToAction("AdminIndex");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var movie = await _movieService.GetByIdAsync(Id);
                if (movie == null)
                {
                    TempData["ErrorMessage"] = "Movie not found.";
                    return RedirectToAction("AdminIndex");
                }

                await _movieService.DeleteAsync(Id);
                TempData["SuccessMessage"] = "Movie deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting movie: " + ex.Message;
            }
            return RedirectToAction("AdminIndex");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            try
            {
                await _movieService.DeleteAsync(Id);
                TempData["SuccessMessage"] = "Movie deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting movie: " + ex.Message;
            }
            return RedirectToAction("AdminIndex");
        }
    }
}
