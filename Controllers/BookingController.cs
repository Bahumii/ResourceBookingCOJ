﻿using Microsoft.AspNetCore.Http;
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

        // booking index page that will pass search name and date for booking filtering
        public ActionResult Index(string searchName, DateTime? searchDate)
        {
            // dynamically retrieving all booking from the db related to resources 
            var bookings = _context.Bookings
                .Include(b => b.Resource)
                .AsQueryable();

            // filter by resource name if search name provided
            if (!string.IsNullOrEmpty(searchName))
                bookings = bookings.Where(b => b.Resource.Name.Contains(searchName));

            // filter by date if search date provided
            if (searchDate.HasValue)
                bookings = bookings.Where(b => b.StartTime.Date == searchDate.Value.Date);

            // view model passed to view that will order bookings by most recent, count of all available resources and upcoming bookings
            var viewModel = new BookingViewModel
            {
                Bookings = bookings.OrderByDescending(b => b.StartTime).ToList(),
                Resources = _context.Resources.ToList(),
                SearchName = searchName,
                SearchDate = searchDate,
            };

            return View(viewModel);
        }

        // booking page page by id
        public ActionResult Details(int id)
        {
            var booking = _context.Bookings
                .Where(b => b.Id == id)
                .Select(b => new Booking
            {
                Id = b.Id,
                ResourceId = b.ResourceId,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                BookedBy = b.BookedBy,
                Purpose = b.Purpose,
                Resource = _context.Resources.FirstOrDefault(r => r.Id == b.ResourceId)
            })
            .FirstOrDefault();

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // create function for addition of new booking record to db
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //initializing resource to check its availability
                    var resource = _context.Resources.FirstOrDefault(r => r.Id == booking.ResourceId);

                    if (resource == null)
                    {
                        TempData["Error"] = "Invalid resource selected.";
                        return RedirectToAction("Index");
                    }

                    // if resource unavailable, then booking wont go through for conflict checking
                    if (!resource.IsAvailable)
                    {
                        TempData["Warning"] = "The selected resource is currently unavailable.";
                        return RedirectToAction("Index");
                    }

                    // declared a boolean value that will compare db values and user input values and return true or false if any conflicts occur
                    bool isConflicted = _context.Bookings.Any(b =>
                    b.ResourceId == booking.ResourceId &&
                    ((booking.StartTime >= b.StartTime && booking.StartTime < b.EndTime) ||
                    (booking.EndTime > b.StartTime && booking.EndTime <= b.EndTime) ||
                    (booking.StartTime <= b.StartTime && booking.EndTime >= b.EndTime)));

                    // if user input contains no conflicts, data will save successfully after comparison
                    if (!isConflicted)
                    {
                        _context.Bookings.Add(booking);
                        _context.SaveChanges();
                        TempData["Success"] = "Resource booked successfully.";
                    }
                    else
                    {
                        TempData["Error"] = "Booking time conflicts with an existing booking in the system. Please try a different time.";
                    }
                }
                catch (Exception E)
                {
                    TempData["Error"] = $"Error adding booking: {E.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Invalid input. Please check input and try again.";
            }

            return RedirectToAction("Index");
        }

        // edit function that actions edit/save or delete depending on button clicked in view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Booking booking, string action)
        {
            try
            {
                // action variable to compare action from front end
                if (action == "save")
                {
                    if (ModelState.IsValid)
                    {
                        // if front end returns save, db will be checked for any conflicts
                        var conflict = _context.Bookings.Any(b =>
                        b.Id != booking.Id &&
                        b.ResourceId == booking.ResourceId &&
                        b.StartTime < booking.EndTime &&
                        booking.StartTime < b.EndTime);

                        // if conflict occurs, error message will pop up
                        if (conflict)
                        {
                            TempData["Error"] = "Time conflict: Resource already booked";
                            return RedirectToAction("Index");
                        }

                        // update/edit will be actioned if no conflicts occur
                        _context.Bookings.Update(booking);
                        _context.SaveChanges();
                        TempData["Success"] = "Booking updated successfully.";
                    }
                    else
                    {
                        TempData["Error"] = "";
                    }
                }
                // delete will be actioned if front end returns delete/cancel
                else if (action == "delete")
                {
                    var b = _context.Bookings.Find(booking.Id);
                    if (b != null)
                    {
                        _context.Bookings.Remove(b);
                        _context.SaveChanges();
                        TempData["Success"] = "Booking cancelled successfully.";
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception E)
            {
                TempData["Error"] = $"Delete failed: {E.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
