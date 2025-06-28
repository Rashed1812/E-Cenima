using BLL.DTO.TIme;
using BLL.Services.ActorService;
using BLL.Services.Movies;
using BLL.Services.TimingService;
using DAL.Data;
using DAL.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Cenima.Controllers
{
    public class TimeController(ITimeService _timeService ,AppDbContext _context) : Controller
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
        [HttpPost]

        [Route("Ticket/BookSeats")] // Make sure route matches your JavaScript call
        public async Task<IActionResult> BookSeats(int Id, [FromBody] List<BookSeatDto> seats)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (seats == null || !seats.Any())
                return BadRequest("No seats selected.");

            try
            {
                var ticketList = seats.Select(seat => new Ticket
                {
                    Timing_Id = Id,
                    RowNumber = seat.Row,
                    SeatNumber = seat.Seat,
                    IsBooked = true,

                }).ToList();

                await _context.Tickets.AddRangeAsync(ticketList);


                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Seats booked successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
    public class BookSeatDto
    {
        public int TimingId { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
    }
}
