using BLL.Services.ActorService;
using BLL.Services.Movies;
using Microsoft.AspNetCore.Mvc;

namespace E_Cenima.Controllers
{
    public class ActorController(IActorService _actorService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var actors = await _actorService.GetAllAsync();
            return View(actors);
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            var actorDetails = await _actorService.GetByIdAsync(id.Value);
            if (actorDetails == null)
            {
                return NotFound();
            }
            return View(actorDetails);
        }
    }
}
