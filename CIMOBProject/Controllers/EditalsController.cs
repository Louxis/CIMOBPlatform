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

namespace CIMOBProject.Controllers
{
    public class EditalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EditalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Editals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Editals.Include(e => e.Document).Include(e => e.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Editals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edital = await _context.Editals
                .Include(e => e.Document)
                .Include(e => e.Employee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (edital == null)
            {
                return NotFound();
            }

            return View(edital);
        }

        // GET: Editals/Create
        public IActionResult Create(string userId)
        {
            ViewData["EmployeeId"] = userId;

            loadHelp();

            return View();
        }

        // POST: Editals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OpenDate,CloseDate,Id,EmployeeId,Title,TextContent,IsPublished,DocumentId")] Edital edital, string link)
        {

            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(link))
                {
                    edital.Document = CreateAndValidateDocument(edital, link);
                }
                //news.DocumentId = doc.DocumentId;
                _context.Add(edital);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "News");
            }
            return RedirectToAction("Index", "News");
        }

        private Document CreateAndValidateDocument(Edital edital, string link)
        {
            Document urlDoc = _context.Documents.Where(d => d.FileUrl.Equals(link)).FirstOrDefault();
            if (urlDoc == null)
            {
                urlDoc = new Document
                {
                    EmployeeId = edital.EmployeeId,
                    Description = "Documento de " + edital.Title,
                    FileUrl = link,
                    UploadDate = DateTime.Now
                };
                _context.Add(urlDoc);
            }
            return urlDoc;
        }


        // GET: Editals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edital = await _context.Editals.Include(n => n.Employee).Include(n => n.Document).SingleOrDefaultAsync(m => m.Id == id);
            if (edital == null)
            {
                return NotFound();
            }

            loadHelp();

            return View(edital);
        }

        // POST: Editals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OpenDate,CloseDate,Id,EmployeeId,Title,TextContent,IsPublished,DocumentId")] Edital edital, string link)
        {
            if (id != edital.Id)
            {
                return NotFound();
            }
            var editalToUpdate = await _context.Editals.Include(n => n.Document).Include(n => n.Employee).SingleOrDefaultAsync(n => n.Id == id);
            editalToUpdate.Title = edital.Title;
            //If it's desired to change employee id to the one updating it
            //newsToUpdate.EmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Document editedDoc = CreateAndValidateDocument(edital, link);
            editedDoc.EmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            editalToUpdate.Document = editedDoc;
            editalToUpdate.TextContent = edital.TextContent;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editalToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditalExists(editalToUpdate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "News");
            }
            return View(editalToUpdate);
        }

        private bool EditalExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }

        // GET: Editals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edital = await _context.Editals
                .Include(e => e.Document)
                .Include(e => e.Employee)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (edital == null)
            {
                return NotFound();
            }

            return View(edital);
        }

        // POST: Editals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var edital = await _context.Editals.SingleOrDefaultAsync(m => m.Id == id);
            _context.Editals.Remove(edital);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void loadHelp()
        {
            ViewData["OpenDateTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 24) as Help).HelpDescription;
            ViewData["CloseDateTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 25) as Help).HelpDescription;
            ViewData["TitleTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 22) as Help).HelpDescription;
            ViewData["TextContentTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 23) as Help).HelpDescription;
            ViewData["DocumentTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 15) as Help).HelpDescription;
        }

    }
}
