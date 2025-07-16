using System.Data.Entity;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ResourceBookingCOJ.Models;

namespace ResourceBookingCOJ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // 
        public IActionResult Index()
        {
            var today = DateTime.Today;

            var upcomingBookings = _context.Bookings
            .Where(b => b.StartTime >= today)
            .OrderBy(b => b.StartTime)
            .Select(b => new Booking
            {
                Id = b.Id,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                BookedBy = b.BookedBy,
                Purpose = b.Purpose,
                ResourceId = b.ResourceId,
                Resource = _context.Resources.FirstOrDefault(r => r.Id == b.ResourceId)
            })
            .ToList();

            ViewBag.TotalResources = _context.Resources.Count();
            ViewBag.TotalBookingsToday = _context.Bookings.Count(b => b.StartTime.Date == today);

            return View(upcomingBookings);
        }
    }
}
