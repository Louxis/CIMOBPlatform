using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;

namespace CIMOBProject.Controllers
{
    /// <summary>
    /// This controller is responsible for all the actions that involve directly TroubleTicketAnswer class.
    /// </summary>
    public class TroubleTicketAnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes controller with the pretended context.
        /// </summary>
        /// <param name="context"></param>
        public TroubleTicketAnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TroubleTicketAnswers.Include(t => t.ApplicationUser).Include(t => t.TroubleTicket);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Create(string applicationUserId, int troubleTicketId)
        {
            ViewData["TroubleTicketId"] = troubleTicketId;
            ViewData["TroubleTicket"] = _context.TroubleTickets
                .Include(t => t.ApplicationUser).Where(t => t.TroubleTicketId == troubleTicketId).SingleOrDefault();
            var application = _context.Applications.Where(a => a.StudentId.Equals(applicationUserId)).OrderBy(a => a.CreationDate).LastOrDefault();
            ViewData["Application"] = "";
            if (application != null)
            {
                ViewData["Application"] = application;
            }
            ViewData["ApplicationUserId"] = applicationUserId;
            ViewData["CreationDate"] = DateTime.Now;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TroubleTicketAnswerId,Content,TroubleTicketId,ApplicationUserId,CreationDate")] TroubleTicketAnswer troubleTicketAnswer, string link)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(link))
                {
                    troubleTicketAnswer.Document = CreateAndValidateDocument(troubleTicketAnswer, link);
                }
                _context.Add(troubleTicketAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "TroubleTickets", new { id = troubleTicketAnswer.TroubleTicketId });
            }
            ViewData["ApplicationUserId"] = troubleTicketAnswer.ApplicationUserId;
            ViewData["TroubleTicketId"] = troubleTicketAnswer.TroubleTicketId;
            ViewData["CreationDate"] = troubleTicketAnswer.CreationDate;
            return View(troubleTicketAnswer);
        }

        /// <summary>
        /// Validates if a document with a link already exists in the database, if it exists will obtain that document and return it.
        /// Otherwise it will create a new document and return the new one.
        /// </summary>
        /// <param name="news">Current news.</param>
        /// <param name="link">Link to validate.</param>
        /// <returns>Valid document.</returns>
        private Document CreateAndValidateDocument(TroubleTicketAnswer troubleTicketAnswer, string link)
        {
            Document urlDoc = _context.Documents
                .Where(d => d.FileUrl.Equals(link)).FirstOrDefault();
            var applicationUser = _context.ApplicationUsers.Where(a => a.Id.Equals(troubleTicketAnswer.ApplicationUserId)).SingleOrDefault();
            if (urlDoc == null)
            {
                if (User.IsInRole("Employee"))
                {
                    urlDoc = new Document
                    {
                        EmployeeId = troubleTicketAnswer.ApplicationUserId,
                        Description = "Documento de " + applicationUser.UserFullname,
                        FileUrl = link,
                        UploadDate = DateTime.Now
                    };
                }else if (User.IsInRole("Student"))
                {
                    var latestApplication = _context.Applications.Include(a => a.Student)
                        .Where(a => a.StudentId.Equals(applicationUser.Id))
                        .OrderBy(a => a.CreationDate).LastOrDefault();
                    urlDoc = new Document
                    {
                        ApplicationId = latestApplication.ApplicationId,
                        Description = "Documento de " + applicationUser.UserFullname,
                        FileUrl = link,
                        UploadDate = DateTime.Now
                    };
                }

                _context.Add(urlDoc);
                _context.SaveChanges();
            }
            return urlDoc;
        }


        private bool TroubleTicketAnswerExists(int id)
        {
            return _context.TroubleTicketAnswers.Any(e => e.TroubleTicketAnswerId == id);
        }
    }
}
