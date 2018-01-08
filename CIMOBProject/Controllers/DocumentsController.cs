using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;
using System.Security.Claims;

namespace CIMOBProject.Controllers {
    public class DocumentsController : Controller {
        private readonly ApplicationDbContext _context;

        private const int MINIMUM_STAT_ID = 3;

        public DocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Documents
        public async Task<IActionResult> Index(int applicationId)
        {
            var latestEdital = _context.Editals.OrderByDescending(e => e.Id).FirstOrDefault();
            
            var applicationContext = _context.Applications.Include(a => a.Student).Where(s => s.ApplicationId == applicationId).FirstOrDefault();
            ViewData["StudentName"] = applicationContext.Student.UserFullname;
            ViewData["ApplicationId"] = applicationId;
            if(User.IsInRole("Employee") && applicationContext.ApplicationStatId < MINIMUM_STAT_ID) 
            {
                ViewData["EvaluationApp"] = "true";
            }
            else
            {
                ViewData["EvaluationApp"] = "false";
            }
            var applicationDbContext = _context.Documents.Include(d => d.Application).Where(s => s.ApplicationId == applicationId && ( latestEdital.OpenDate <= s.Application.CreationDate) && (s.Application.CreationDate <= latestEdital.CloseDate));
            if (applicationDbContext == null)
            {
                return NotFound();
            }
            
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
                .Include(d => d.Application)
                .SingleOrDefaultAsync(m => m.DocumentId == id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public IActionResult Create(int applicationId)
        {
            //var latestEdital = _context.Editals.OrderByDescending(a => a.Id).FirstOrDefault();
            ViewData["ApplicationId"] = applicationId;
            Application application = _context.Applications.Where(a => a.ApplicationId == applicationId).FirstOrDefault();
            if (application != null) {
                if (User.IsInRole("Employee") && application.ApplicationStatId < MINIMUM_STAT_ID) {
                    return RedirectToAction("Application", "Home", new { message = "Não pode carregar documentos se o aluno se encontra em avaliação." });
                }
            }            
            //ViewData["Date"] = DateTime.Now;
            loadHelp();
            return View();
        }      

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentId,Description,FileUrl,UploadDate,ApplicationId")] Document document)
        {
            if (ModelState.IsValid)
            {
                document.UploadDate = DateTime.Now;
                if (User.IsInRole("Employee")) 
                {
                    document.EmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                _context.Add(document);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Documents", new { applicationId = document.ApplicationId.GetValueOrDefault() });
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
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id", document.Application.StudentId);
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
            ViewData["Id"] = new SelectList(_context.ApplicationUsers, "Id", "Id", document.Application.StudentId);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Documents.SingleOrDefaultAsync(m => m.DocumentId == id);

            if (User.IsInRole("Employee"))
            {
                return RedirectToAction("Application", "Home", new { message = "Não tem permissão para aceder a esta funcionalidade" });
            }
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
            var applicationId = document.ApplicationId;
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction( "Index", "Documents", new { applicationId = applicationId });

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