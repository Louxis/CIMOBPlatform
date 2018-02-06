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
            ViewData["ApplicationUserId"] = applicationUserId;
            ViewData["CreationDate"] = DateTime.Now;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TroubleTicketAnswerId,Content,TroubleTicketId,ApplicationUserId,CreationDate")] TroubleTicketAnswer troubleTicketAnswer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(troubleTicketAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "TroubleTickets", new { id = troubleTicketAnswer.TroubleTicketId });
            }
            ViewData["ApplicationUserId"] = troubleTicketAnswer.ApplicationUserId;
            ViewData["TroubleTicketId"] = troubleTicketAnswer.TroubleTicketId;
            ViewData["CreationDate"] = troubleTicketAnswer.CreationDate;
            return View(troubleTicketAnswer);
        }

        private bool TroubleTicketAnswerExists(int id)
        {
            return _context.TroubleTicketAnswers.Any(e => e.TroubleTicketAnswerId == id);
        }
    }
}
