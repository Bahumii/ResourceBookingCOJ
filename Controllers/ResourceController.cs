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

        // GET: ResourceController
        public ActionResult Index()
        {
            var resources = _context.Resources.ToList();

            return View(resources);
        }

        // GET: ResourceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResourceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResourceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(/*IFormCollection collection*/ Resource resource)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Resources.Add(resource);
                    _context.SaveChanges();     
                }
                return RedirectToAction("Index");
            }
            catch (Exception E)
            {
                return View();
            }
        }

        // GET: ResourceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResourceController/Edit/5
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

        // GET: ResourceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResourceController/Delete/5
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
