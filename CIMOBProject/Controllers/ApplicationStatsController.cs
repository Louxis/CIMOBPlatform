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
    public class ApplicationStatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationStatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationStats
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationStats.ToListAsync());
        }

        // GET: ApplicationStats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationStat = await _context.ApplicationStats
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationStat == null)
            {
                return NotFound();
            }

            return View(applicationStat);
        }

        // GET: ApplicationStats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationStats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ApplicationStat applicationStat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationStat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationStat);
        }

        // GET: ApplicationStats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationStat = await _context.ApplicationStats.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationStat == null)
            {
                return NotFound();
            }
            return View(applicationStat);
        }

        // POST: ApplicationStats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ApplicationStat applicationStat)
        {
            if (id != applicationStat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationStat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationStatExists(applicationStat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationStat);
        }

        // GET: ApplicationStats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationStat = await _context.ApplicationStats
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationStat == null)
            {
                return NotFound();
            }

            return View(applicationStat);
        }

        // POST: ApplicationStats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationStat = await _context.ApplicationStats.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationStats.Remove(applicationStat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationStatExists(int id)
        {
            return _context.ApplicationStats.Any(e => e.Id == id);
        }
    }
}
