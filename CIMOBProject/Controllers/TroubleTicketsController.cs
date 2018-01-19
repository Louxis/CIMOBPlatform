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
    /// This controller is responsible for all the actions that involve directly TroubleTicket class.
    /// </summary>
    public class TroubleTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TroubleTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This Method filters TroubleTicket's table depending on who is accessing it and what kind of filter
        /// user chooses.
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <param name="ticketFilter">Filter that is being used. If null, no filter is applied</param>
        /// <param name="listOrder">Order in which the table is presented. If null, the order is from latest entry to earliest entry</param>
        /// <returns>Index View Filtered</returns>
        // GET: TroubleTickets
        public async Task<IActionResult> Index(string userId, string ticketFilter, string listOrder)
        {
            
            var applicationDbContext = _context.TroubleTickets.Include(t => t.ApplicationUser).Include(t =>t.Answers);
            if (User.IsInRole("Student"))
            {
                var student = _context.Students.Where(s => s.Id == userId).SingleOrDefault();
                var studentTroubleTickets = applicationDbContext.Where(t => t.ApplicationUserId == userId || t.StudentNumber == student.StudentNumber);
                if (String.IsNullOrEmpty(ticketFilter))
                {
                    if (String.IsNullOrEmpty(listOrder))
                    {
                        return View(await studentTroubleTickets.OrderByDescending(t => t.CreationDate).ToListAsync());
                    }
                    else
                    {
                        return View(await studentTroubleTickets.OrderBy(t => t.CreationDate).ToListAsync());
                    }
                    
                }
                if (ticketFilter.Equals("Sent"))
                {
                    studentTroubleTickets = studentTroubleTickets.Where(t => t.ApplicationUserId == userId);
                }
                else if (ticketFilter.Equals("Received"))
                {
                    studentTroubleTickets = studentTroubleTickets.Where(t => t.StudentNumber == student.StudentNumber);
                }
                else if (ticketFilter.Equals("Opened"))
                {
                    studentTroubleTickets = studentTroubleTickets.Where(t => t.Solved == false);
                }
                else if (ticketFilter.Equals("Closed"))
                {
                    studentTroubleTickets = studentTroubleTickets.Where(t => t.Solved == true);
                }
                if (String.IsNullOrEmpty(listOrder))
                {
                    return View(await studentTroubleTickets.OrderByDescending(t => t.CreationDate).ToListAsync());
                }
                else
                {
                    return View(await studentTroubleTickets.OrderBy(t => t.CreationDate).ToListAsync());
                }
            }
            else if (User.IsInRole("Employee"))
            { 
                var troubleTickets = applicationDbContext.Where(t => t.ApplicationUserId == userId || String.IsNullOrEmpty(t.StudentNumber));
                if (String.IsNullOrEmpty(ticketFilter))
                {
                    if (String.IsNullOrEmpty(listOrder))
                    {
                        return View(await troubleTickets.OrderByDescending(t => t.CreationDate).ToListAsync());
                    }
                    else
                    {
                        return View(await troubleTickets.OrderBy(t => t.CreationDate).ToListAsync());
                    }
                }
                if (ticketFilter.Equals("Sent"))
                {
                    troubleTickets = troubleTickets.Where(t => t.ApplicationUserId == userId);
                }else if (ticketFilter.Equals("Received")){
                    troubleTickets = troubleTickets.Where(t => String.IsNullOrEmpty(t.StudentNumber));
                }else if(ticketFilter.Equals("Opened")){
                    troubleTickets = troubleTickets.Where(t => t.Solved == false);;
                }else if (ticketFilter.Equals("Closed"))
                {
                    troubleTickets = troubleTickets.Where(t => t.Solved == true);
                }
                if (String.IsNullOrEmpty(listOrder))
                {
                    return View(await troubleTickets.OrderByDescending(t => t.CreationDate).ToListAsync());
                }
                else
                {
                    return View(await troubleTickets.OrderBy(t => t.CreationDate).ToListAsync());
                }
            }
            else
            {
                //to error this.
                return View();
            }
        }

        /// <summary>
        /// This method changes a TroubleTicket's Solved attribute to true.
        /// </summary>
        /// <param name="applicationUserId">ID of the user that is logged in. Used to redirect to the right Index View.</param>
        /// <param name="troubleTicketId">ID of the trouble ticket to change.</param>
        /// <returns>Index View</returns>
        public async Task<IActionResult> CloseTicket(string applicationUserId, int troubleTicketId)
        {
            var getTroubleTicket = _context.TroubleTickets.SingleOrDefault(a => a.TroubleTicketId == troubleTicketId);

            getTroubleTicket.Solved = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "TroubleTickets", new { userId = applicationUserId });
        }

        // GET: TroubleTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troubleTicket = await _context.TroubleTicket
                .Include(t => t.ApplicationUser)
                .SingleOrDefaultAsync(m => m.TroubleTicketId == id);
            var answersList = _context.TroubleTicketAnswers.Include(t => t.ApplicationUser).Include(t => t.TroubleTicket).Where(t => t.TroubleTicketId == id).ToList();
            ViewData["AnswersList"] = answersList;
            if (troubleTicket == null)
            {
                return NotFound();
            }

            return View(troubleTicket);
        }

        // GET: TroubleTickets/Create
        public IActionResult Create(String userId, String studentNumber)
        {
            ViewData["StudentNumber"] = studentNumber;
            ViewData["ApplicationUserId"] = userId;
            ViewData["CreationDate"] = DateTime.Now;
            return View();
        }

        // POST: TroubleTickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TroubleTicketId,Title,Description,CreationDate,Solved,ApplicationUserId,StudentNumber")] TroubleTicket troubleTicket)
        {
            
            if (User.IsInRole("Employee"))
            {
               if( _context.Students.Where(a => a.StudentNumber.Equals(troubleTicket.StudentNumber)).FirstOrDefault() == null)
                {
                    ViewData["ApplicationUserId"] = _context.ApplicationUsers.Where(a => a.Id == troubleTicket.ApplicationUserId).SingleOrDefault();
                    ViewData["ErrorMessage"] = "Não existe um estudante com esse número";
                    return View(troubleTicket);
                }
            }
            if (ModelState.IsValid)
            {
                troubleTicket.Answers = new List<TroubleTicketAnswer>();
                _context.Add(troubleTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "TroubleTickets", new { userId = troubleTicket.ApplicationUserId });
            }
            ViewData["ErrorMessage"] = "";
            ViewData["ApplicationUserId"] = _context.ApplicationUsers.Where(a => a.Id == troubleTicket.ApplicationUserId).SingleOrDefault();
            return View(troubleTicket);
        }

        private bool TroubleTicketExists(int id)
        {
            return _context.TroubleTicket.Any(e => e.TroubleTicketId == id);
        }
    }
}
