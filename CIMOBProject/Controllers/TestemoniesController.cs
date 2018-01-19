using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;

namespace CIMOBProject.Controllers
{
    /// <summary>
    /// This controller is responsible for all the actions that involve directly Testemony class.
    /// </summary>
    public class TestemoniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestemoniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Testemonies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Testemonies.Include(t => t.ApplicationUser).OrderByDescending(t => t.CreationDate);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// This method changes Valid attribute in class Testemony to True.
        /// </summary>
        /// <param name="testemonyId">ID of the testemony to change Valid attibute</param>
        /// <returns>The View Index</returns>
        public async Task<IActionResult> ValidateTestemony(int testemonyId)
        {
            var testemony = _context.Testemonies.Where(t => t.TestemonyId == testemonyId).SingleOrDefault();
            testemony.Valid = true;
            _context.Update(testemony);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Testemonies");
        }

        // GET: Testemonies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testemony = await _context.Testemonies
                .Include(t => t.ApplicationUser)
                .SingleOrDefaultAsync(m => m.TestemonyId == id);
            if (testemony == null)
            {
                return NotFound();
            }

            return View(testemony);
        }


        // GET: Testemonies/Create
        public IActionResult Create(string userId)
        {
            ViewData["ApplicationUserId"] = userId;
            ViewData["CreationDate"] = DateTime.Now;
            return View();
        }

        // POST: Testemonies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestemonyId,Title,Content,ApplicationUserId,Valid,CreationDate")] Testemony testemony)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testemony);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = testemony.ApplicationUserId;
            ViewData["CreationDate"] = testemony.CreationDate;
            return View(testemony);
        }

        // GET: Testemonies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testemony = await _context.Testemonies
                .Include(t => t.ApplicationUser)
                .SingleOrDefaultAsync(m => m.TestemonyId == id);
            if (testemony == null)
            {
                return NotFound();
            }

            return View(testemony);
        }

        // POST: Testemonies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testemony = await _context.Testemonies.SingleOrDefaultAsync(m => m.TestemonyId == id);
            _context.Testemonies.Remove(testemony);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestemonyExists(int id)
        {
            return _context.Testemonies.Any(e => e.TestemonyId == id);
        }
    }
}
