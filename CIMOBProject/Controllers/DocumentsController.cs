using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;

namespace CIMOBProject.Controllers {
    public class DocumentsController : Controller {
        private readonly ApplicationDbContext _context;

        public DocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Documents
        public async Task<IActionResult> Index(string userId)
        {
            ViewData["StudentName"] = _context.Students.Where(s => s.Id.Equals(userId)).FirstOrDefault().UserFullname;
            var applicationDbContext = _context.Documents.Include(d => d.ApplicationUser).Where(s => s.Id.Equals(userId));
            if (applicationDbContext == null)
            {
                return NotFound();
            }
            //ViewData["studentName"] = applicationDbContext.First().Student.UserFullname;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.ApplicationUser)
                .SingleOrDefaultAsync(m => m.DocumentId == id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create(string userId)
        {
            ViewData["Id"] = userId;
            //ViewData["Date"] = DateTime.Now;
            loadHelp();
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentId,Description,FileUrl,UploadDate,Id")] Document document)
        {
            string currentUserId = "";
            if (ModelState.IsValid)
            {
                document.UploadDate = DateTime.Now;
                currentUserId = document.Id;
                _context.Add(document);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Documents", new { applicationUserId = currentUserId });
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.SingleOrDefaultAsync(m => m.DocumentId == id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Students, "Id", "Id", document.Id);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentId,Description,FileUrl,UploadDate,Id")] Document document)
        {
            if (id != document.DocumentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.DocumentId))
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
            ViewData["Id"] = new SelectList(_context.Students, "Id", "Id", document.Id);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .Include(d => d.ApplicationUser)
                .SingleOrDefaultAsync(m => m.DocumentId == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Documents.SingleOrDefaultAsync(m => m.DocumentId == id);
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.DocumentId == id);
        }

        private void loadHelp()
        {
            ViewData["DescriptionTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 14) as Help).HelpDescription;
            ViewData["FileURLTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 15) as Help).HelpDescription;
        }
    }
}