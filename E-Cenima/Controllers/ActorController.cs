using BLL.DTO.Actors;
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
        public async Task<IActionResult> AdminIndex()
        {
            var actors = await _actorService.GetAllAsync();
            return View("AdminIndex",actors);
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
        [HttpGet]
        public async Task<IActionResult> AddActor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddActor(ActorAddDto actorAddDto)
        {
            try
            {
                await _actorService.AddAsync(actorAddDto);
                return await AdminIndex();
            }
            catch (Exception ex)
            {
                return View(actorAddDto);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditActor(int Id)
        {
            try
            {
                var actor = await _actorService.GetByIdAsync(Id);
                var actorEditDto = new ActorEditDto
                {
                    Id = actor.Id,
                    FullName = actor.FullName,
                    Bio = actor.Bio,
                    ProfilePictureURL = actor.ProfilePictureURL

                };
                return View(actorEditDto);  

            }
            catch (Exception ex) 
            {
                return NotFound(ex);
            }
            

        }
        [HttpPost]
        public async Task<IActionResult> EditActor(ActorEditDto actor)
        {
            try
            {
                await _actorService.EditAsync(actor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return await AdminIndex();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteActor(int Id)
        {
            try
            {
                await _actorService.RemoveAsync(Id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return  await AdminIndex();
       }



    }
}
