using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceBookingCOJ.Models;
using System.Data.Entity;

namespace ResourceBookingCOJ.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookingController
        public ActionResult Index(string searchName, DateTime? searchDate)
        {
            var bookings = _context.Bookings
                .Include(b => b.Resource)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
                bookings = bookings.Where(b => b.Resource.Name.Contains(searchName));

            if (searchDate.HasValue)
                bookings = bookings.Where(b => b.StartTime.Date == searchDate.Value.Date);

            ViewBag.Resources = _context.Resources.ToList();
            ViewBag.SearchName = searchName;
            ViewBag.SearchDate = searchDate?.ToString("yyyy-MM-dd");

            return View(bookings.ToList());
        }

        // GET: BookingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
