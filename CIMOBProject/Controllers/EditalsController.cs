using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;
using System.Security.Claims;
using CIMOBProject.Services;
using System;

namespace CIMOBProject.Controllers
{
    /// <summary>
    /// This controller is responsible for all the actions related to Editals (related to news).
    /// </summary>
    public class EditalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes controller with the pretended context
        /// </summary>
        /// <param name="context"></param>
        public EditalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Editals.Include(e => e.Document).Include(e => e.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

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

        /// <summary>
        /// Action that leads to Edital creation views. 
        /// This view will also contain an input for an optional document the employee can upload.
        /// It's up to the employee to validate such url.
        /// </summary>
        /// <param name="userId">Logged in employee id.</param>
        /// <returns>Edital creation view.</returns>
        public IActionResult Create(string userId)
        {
            ViewData["EmployeeId"] = userId;
            loadHelp();
            return View();
        }

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
                EmailSender emailSender = new EmailSender();
                List<string> emails = _context.Students.Select(s => s.Email).ToList();
                emailSender.SendMultipleEmail(emails, "Edital Publicado", "Saudações a todos os estudantes, " +
                    "encontra-se disponivel na plataforma o edital para as novas inscrições de Erasmus nas datas " +
                    edital.OpenDate.ToShortDateString() + " e " + edital.CloseDate.ToShortDateString() + ". Obrigado, CIMOB");
                return RedirectToAction("Index", "News");
            }
            return View(edital);
        }

        /// <summary>
        /// Validates if a document with a link already exists in the database, if it exists will obtain that document and return it.
        /// Otherwise it will create a new document and return the new one.
        /// </summary>
        /// <param name="edital">Current edital.</param>
        /// <param name="link">Link to validate.</param>
        /// <returns>Valid document.</returns>
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
            ViewData["OpenDateTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "OpenDate") as Help).HelpDescription;
            ViewData["CloseDateTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "CloseDate") as Help).HelpDescription;
            ViewData["TitleTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "Title") as Help).HelpDescription;
            ViewData["TextContentTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "TextContent") as Help).HelpDescription;
            ViewData["DocumentTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "FileURL") as Help).HelpDescription;
        }

        private bool EditalExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
