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
                .Include(b => b.Resource)
                .Where(b => b.StartTime >= today)
                .OrderBy(b => b.StartTime)
                .ToList();

            ViewBag.TotalResources = _context.Resources.Count();
            ViewBag.TotalBookingsToday = _context.Bookings.Count(b => b.StartTime.Date == today);

            return View(upcomingBookings);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
