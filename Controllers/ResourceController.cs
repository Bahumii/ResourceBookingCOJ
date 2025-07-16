using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceBookingCOJ.Models;

namespace ResourceBookingCOJ.Controllers
{
    public class ResourceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResourceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // resources index page 
        public ActionResult Index()
        {
            var viewModel = new ResourceViewModel
            {
                ResourceList = _context.Resources.ToList()
            };

            return View(viewModel);
        }

        // resources details page by id
        public ActionResult Details(int id)
        {
            var resource = _context.Resources.FirstOrDefault(r => r.Id == id);
            if (resource == null)
            {
                return NotFound();
            }
            return View(resource);
        }

        // resources create function
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Resource resource)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Resources.Add(resource);
                    _context.SaveChanges();
                    TempData["Success"] = "Resource added successfully.";
                }
                catch (Exception E)
                {
                    TempData["Error"] = $"Error adding resource: {E.Message}";
                }
            }
            else
            {
                TempData["Error"] = "Invalid input. Please check input and try again.";
            }

            return RedirectToAction("Index");
        }

        // edit function that will action delete or edit based on front end input from buttons
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Resource resource, string action)
        {
            try
            {
                //action variable to compare action from front end
                if (action == "save")
                {
                    if (ModelState.IsValid)
                    {
                        //performs record edit if action passed to function = save
                        _context.Resources.Update(resource);
                        _context.SaveChanges();
                        TempData["Success"] = "Resource updated successfully";
                    }
                    else
                    {
                        TempData["Error"] = "Resource update failed. Please check your input and try again.";
                    }
                }
                else if (action == "delete")
                {
                    //performs record delete if action passed = delete
                    var r = _context.Resources.Find(resource.Id);
                    if (r != null)
                    {
                        _context.Resources.Remove(r);
                        _context.SaveChanges();
                        TempData["Success"] = "Resource deleted successfully";
                    }
                }
            }
            catch (Exception E)
            {
                TempData["Error"] = $"Delete failed: {E.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
