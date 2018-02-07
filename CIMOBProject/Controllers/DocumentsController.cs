using System;
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
    /// <summary>
    /// This controller is responsible for all the actions related to documents.
    /// </summary>
    public class DocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int MINIMUM_STAT_ID = 3;
        private const int REJECTED_STAT_ID = 5;

        /// <summary>
        /// Initializes controller with the pretended context
        /// </summary>
        /// <param name="context"></param>
        public DocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Documents
        /// <summary>
        /// Indexes all the documents present in a specific application. 
        /// Documents are contained in a url to be hosted by the user.
        /// </summary>
        /// <param name="applicationId">Specified Application.</param>
        /// <returns>View with document list.</returns>
        public async Task<IActionResult> Index(int applicationId)
        {
            var latestEdital = _context.Editals.OrderByDescending(e => e.Id).FirstOrDefault();
            var application = _context.Applications.Include(a => a.Student)
                .Where(s => s.ApplicationId == applicationId).FirstOrDefault();
            ViewData["StudentName"] = application.Student.UserFullname;
            ViewData["ApplicationId"] = applicationId;

            if (User.IsInRole("Employee") &&
                (application.ApplicationStatId < MINIMUM_STAT_ID ||
                application.ApplicationStatId == REJECTED_STAT_ID))
            {
                ViewData["EvaluationApp"] = "true";
            }
            else
            {
                ViewData["EvaluationApp"] = "false";
            }
            var applications = _context.Documents.Include(d => d.Application)
                .Where(s => s.ApplicationId == applicationId && (latestEdital.OpenDate <= s.Application.CreationDate)
                    && (s.Application.CreationDate <= latestEdital.CloseDate));
            if (applications == null)
            {
                return NotFound();
            }

            return View(await applications.ToListAsync());
        }

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

        /// <summary>
        /// Action that leads to Document creation view.
        /// Employees can also access this view if the student is in mobility, otherwise only the student can.
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns>Document creation view</returns>
        public IActionResult Create(int applicationId)
        {
            ViewData["ApplicationId"] = applicationId;
            Application application = _context.Applications.Where(a => a.ApplicationId == applicationId).FirstOrDefault();
            if (application != null)
            {
                if (User.IsInRole("Employee") &&
                (application.ApplicationStatId < MINIMUM_STAT_ID ||
                application.ApplicationStatId == REJECTED_STAT_ID))
                {
                    return RedirectToAction("Application", "Home", new { message = "Não pode carregar documentos se o aluno se encontra em avaliação." });
                }
            }
            loadHelp();
            return View();
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Documents.SingleOrDefaultAsync(m => m.DocumentId == id);
            var applicationId = document.ApplicationId;
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Documents", new { applicationId = applicationId });
        }

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.DocumentId == id);
        }

        private void loadHelp()
        {
            ViewData["DescriptionTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "Description") as Help).HelpDescription;
            ViewData["FileURLTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "FileURL") as Help).HelpDescription;
        }
    }
}