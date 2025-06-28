using Microsoft.AspNetCore.Mvc;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace E_Cenima.Controllers
{
    public class TicketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Show seat selection for a specific timing
        public async Task<IActionResult> SeatSelection(int timingId)
        {
            try
            {
                var timing = await _context.Timings
                    .Include(t => t.Showtime)
                        .ThenInclude(s => s.Cinema)
                    .Include(t => t.Showtime)
                        .ThenInclude(s => s.Movie)
                    .Include(t => t.Tickets)
                    .FirstOrDefaultAsync(t => t.Id == timingId);

                if (timing == null)
                {
                    TempData["ErrorMessage"] = "Timing not found.";
                    return RedirectToAction("Index", "Movie");
                }

                // Generate seats if they don't exist
                await GenerateSeatsForTiming(timingId, timing.Showtime.Cinema.Capacity);

                // Get all tickets for this timing
                var tickets = await _context.Tickets
                    .Where(t => t.Timing_Id == timingId)
                    .OrderBy(t => t.RowNumber)
                    .ThenBy(t => t.SeatNumber)
                    .ToListAsync();

                ViewBag.Timing = timing;
                ViewBag.Cinema = timing.Showtime.Cinema;
                ViewBag.Movie = timing.Showtime.Movie;
                ViewBag.ShowDate = timing.Showtime.Date;

                return View(tickets);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading seat selection: " + ex.Message;
                return RedirectToAction("Index", "Movie");
            }
        }

        // POST: Book selected seats
        [HttpPost]
        public async Task<IActionResult> BookSeats(int timingId, List<int> selectedSeats)
        {
            try
            {
                if (!selectedSeats.Any())
                {
                    TempData["ErrorMessage"] = "Please select at least one seat.";
                    return RedirectToAction("SeatSelection", new { timingId });
                }

                var timing = await _context.Timings
                    .Include(t => t.Showtime)
                        .ThenInclude(s => s.Cinema)
                    .Include(t => t.Showtime)
                        .ThenInclude(s => s.Movie)
                    .FirstOrDefaultAsync(t => t.Id == timingId);

                if (timing == null)
                {
                    TempData["ErrorMessage"] = "Timing not found.";
                    return RedirectToAction("Index", "Movie");
                }

                // Check if user is logged in
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    TempData["ErrorMessage"] = "Please log in to book tickets.";
                    return RedirectToAction("Login", "Account");
                }

                // Check if selected seats are available
                var tickets = await _context.Tickets
                    .Where(t => selectedSeats.Contains(t.Ticket_Id) && t.Timing_Id == timingId)
                    .ToListAsync();

                if (tickets.Any(t => t.IsBooked))
                {
                    TempData["ErrorMessage"] = "Some selected seats are already booked. Please select different seats.";
                    return RedirectToAction("SeatSelection", new { timingId });
                }

                // Calculate total price
                decimal totalPrice = tickets.Sum(t => t.SeatType == SeatType.Premium ? timing.Price * 1.5m : timing.Price);

                // Create order
                var order = new TicketOrder
                {
                    UserId = userId,
                    Amount = tickets.Count,
                    TotalPrice = totalPrice,
                    PaymentStatus = "Pending",
                    BookingTime = DateTime.Now
                };

                _context.TicketOrders.Add(order);
                await _context.SaveChangesAsync();

                // Update tickets
                foreach (var ticket in tickets)
                {
                    ticket.IsBooked = true;
                    ticket.Order_Id = order.Order_Id;
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Successfully booked {tickets.Count} seat(s)! Total: ${totalPrice:F2}";
                return RedirectToAction("BookingConfirmation", new { orderId = order.Order_Id });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error booking seats: " + ex.Message;
                return RedirectToAction("SeatSelection", new { timingId });
            }
        }

        // GET: Booking confirmation
        public async Task<IActionResult> BookingConfirmation(int orderId)
        {
            try
            {
                var order = await _context.TicketOrders
                    .Include(o => o.User)
                    .Include(o => o.Tickets)
                        .ThenInclude(t => t.timing)
                            .ThenInclude(t => t.Showtime)
                                .ThenInclude(s => s.Movie)
                    .Include(o => o.Tickets)
                        .ThenInclude(t => t.timing)
                            .ThenInclude(t => t.Showtime)
                                .ThenInclude(s => s.Cinema)
                    .FirstOrDefaultAsync(o => o.Order_Id == orderId);

                if (order == null)
                {
                    TempData["ErrorMessage"] = "Order not found.";
                    return RedirectToAction("Index", "Movie");
                }

                // Check if user owns this order
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (order.UserId != userId)
                {
                    TempData["ErrorMessage"] = "Access denied.";
                    return RedirectToAction("Index", "Movie");
                }

                return View(order);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading booking confirmation: " + ex.Message;
                return RedirectToAction("Index", "Movie");
            }
        }

        // GET: User's booking history
        public async Task<IActionResult> MyBookings()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account");
                }

                var orders = await _context.TicketOrders
                    .Include(o => o.Tickets)
                        .ThenInclude(t => t.timing)
                            .ThenInclude(t => t.Showtime)
                                .ThenInclude(s => s.Movie)
                    .Include(o => o.Tickets)
                        .ThenInclude(t => t.timing)
                            .ThenInclude(t => t.Showtime)
                                .ThenInclude(s => s.Cinema)
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.BookingTime)
                    .ToListAsync();

                return View(orders);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading bookings: " + ex.Message;
                return RedirectToAction("Index", "Movie");
            }
        }

        // Helper method to generate seats for a timing
        private async Task GenerateSeatsForTiming(int timingId, int capacity)
        {
            var existingTickets = await _context.Tickets
                .Where(t => t.Timing_Id == timingId)
                .CountAsync();

            if (existingTickets > 0)
                return; // Seats already generated

            var tickets = new List<Ticket>();
            int seatsPerRow = 10; // Configurable
            int totalRows = (int)Math.Ceiling((double)capacity / seatsPerRow);

            for (int row = 1; row <= totalRows; row++)
            {
                int seatsInThisRow = Math.Min(seatsPerRow, capacity - ((row - 1) * seatsPerRow));
                
                for (int seat = 1; seat <= seatsInThisRow; seat++)
                {
                    var seatType = (row <= 3) ? SeatType.Premium : SeatType.Regular;
                    
                    tickets.Add(new Ticket
                    {
                        RowNumber = row,
                        SeatNumber = seat,
                        Timing_Id = timingId,
                        IsBooked = false,
                        SeatType = seatType
                    });
                }
            }

            _context.Tickets.AddRange(tickets);
            await _context.SaveChangesAsync();
        }

        // Admin: View all bookings
        public async Task<IActionResult> AdminBookings()
        {
            try
            {
                var orders = await _context.TicketOrders
                    .Include(o => o.User)
                    .Include(o => o.Tickets)
                        .ThenInclude(t => t.timing)
                            .ThenInclude(t => t.Showtime)
                                .ThenInclude(s => s.Movie)
                    .Include(o => o.Tickets)
                        .ThenInclude(t => t.timing)
                            .ThenInclude(t => t.Showtime)
                                .ThenInclude(s => s.Cinema)
                    .OrderByDescending(o => o.BookingTime)
                    .ToListAsync();

                return View(orders);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error loading admin bookings: " + ex.Message;
                return View(new List<TicketOrder>());
            }
        }

        // Admin: Update payment status
        [HttpPost]
        public async Task<IActionResult> UpdatePaymentStatus(int orderId, string status)
        {
            try
            {
                var order = await _context.TicketOrders.FindAsync(orderId);
                if (order != null)
                {
                    order.PaymentStatus = status;
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Payment status updated successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Order not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error updating payment status: " + ex.Message;
            }

            return RedirectToAction("AdminBookings");
        }
    }
}

