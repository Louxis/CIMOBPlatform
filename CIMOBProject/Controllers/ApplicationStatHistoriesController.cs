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
    public class ApplicationStatHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationStatHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationStatHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicationStatHistory.Include(a => a.Application);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApplicationStatHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationStatHistory = await _context.ApplicationStatHistory
                .Include(a => a.Application)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationStatHistory == null)
            {
                return NotFound();
            }

            return View(applicationStatHistory);
        }

        // GET: ApplicationStatHistories/Create
        public IActionResult Create()
        {
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "ApplicationId");
            return View();
        }

        // POST: ApplicationStatHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationId,ApplicationStat,DateOfUpdate")] ApplicationStatHistory applicationStatHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationStatHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "ApplicationId", applicationStatHistory.ApplicationId);
            return View(applicationStatHistory);
        }

        // GET: ApplicationStatHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationStatHistory = await _context.ApplicationStatHistory.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationStatHistory == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "ApplicationId", applicationStatHistory.ApplicationId);
            return View(applicationStatHistory);
        }

        // POST: ApplicationStatHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationId,ApplicationStat,DateOfUpdate")] ApplicationStatHistory applicationStatHistory)
        {
            if (id != applicationStatHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationStatHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationStatHistoryExists(applicationStatHistory.Id))
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
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "ApplicationId", "ApplicationId", applicationStatHistory.ApplicationId);
            return View(applicationStatHistory);
        }

        // GET: ApplicationStatHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationStatHistory = await _context.ApplicationStatHistory
                .Include(a => a.Application)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationStatHistory == null)
            {
                return NotFound();
            }

            return View(applicationStatHistory);
        }

        // POST: ApplicationStatHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationStatHistory = await _context.ApplicationStatHistory.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationStatHistory.Remove(applicationStatHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationStatHistoryExists(int id)
        {
            return _context.ApplicationStatHistory.Any(e => e.Id == id);
        }
    }
}
