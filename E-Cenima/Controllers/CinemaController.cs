using BLL.DTO.Actors;
using BLL.DTO.Cinema;
using BLL.Services.CinemaService;
using Microsoft.AspNetCore.Mvc;

namespace E_Cenima.Controllers
{
    public class CinemaController(ICenimaService _cinemaService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var cinemas = await _cinemaService.GetCinemasWithTimes();
            return View(cinemas);
        }
        public async Task<IActionResult> AdminIndex()
        {
            var cinemas = await _cinemaService.GetAllCinemas();

            return View("AdminIndex", cinemas);
        }
        [HttpGet]
        public async Task<IActionResult> AdminAdd()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminAdd(CinemaAddDto cinemaAdd)
        {
            try
            {
                await _cinemaService.AddCinema(cinemaAdd);
                return await AdminIndex();
            }
            catch (Exception ex)
            {
                return View(cinemaAdd);
            }

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                var Cinema = await _cinemaService.GetCinemaById(Id);
                var CinemEdit = new CinemaEditDto
                {
                    Id = Cinema.Id,
                    Name = Cinema.Name,
                    Address = Cinema.Address,
                    Capacity = Cinema.Capacity,
                    Logo = Cinema.Logo,
                };
                return  View(CinemEdit);
            }catch(Exception ex)
            {
                return await AdminIndex();
            }
           
        }
         [HttpPost]
        public async Task<IActionResult> Edit(CinemaEditDto cinemaEdit)
        {
            try
            {
            await _cinemaService.Update(cinemaEdit);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return await AdminIndex();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _cinemaService.RemoveCinema(Id);
                return await AdminIndex();
            }
            catch (Exception ex)
            {
                return  NotFound(ex);
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            var cinemaDetails = await _cinemaService.GetCinemaById(id.Value);
            if (cinemaDetails == null)
            {
                return NotFound();
            }
            return View(cinemaDetails);
        }
    }
}
