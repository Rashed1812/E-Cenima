using BLL.DTO.TIme;
using BLL.Services.ActorService;
using BLL.Services.Movies;
using BLL.Services.TimingService;
using Microsoft.AspNetCore.Mvc;

namespace E_Cenima.Controllers
{
    public class TimeController(ITimeService _timeService) : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ShowTicket(int Id)
        {

            var time = await _timeService.GetTicketByTimeId(Id);

            return View(time);
        }
    }
}
