using BLL.Services.CinemaService;
using Microsoft.AspNetCore.Mvc;

namespace E_Cenima.Controllers
{
    public class CinemaController(ICenimaService _cenimaService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var cinemas = await _cenimaService.GetAllCinemas();
            return View(cinemas);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            var cinemaDetails = await _cenimaService.GetCinemaById(id.Value);
            if (cinemaDetails == null)
            {
                return NotFound();
            }
            return View(cinemaDetails);
        }
    }
}
