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

        /// <summary>
        /// Initializes controller with the pretended context.
        /// </summary>
        /// <param name="context"></param>
        public TroubleTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string userId, string ticketFilter)
        {
            
            var applicationDbContext = _context.TroubleTickets.Include(t => t.ApplicationUser).Include(t =>t.Answers);
            if (User.IsInRole("Student"))
            {
                var student = _context.Students.Where(s => s.Id == userId).SingleOrDefault();
                student.IsNotified = false;
                var studentTroubleTickets = applicationDbContext.Where(t => t.ApplicationUserId == userId || t.StudentNumber == student.StudentNumber);
                if (String.IsNullOrEmpty(ticketFilter))
                {
                    return View(await studentTroubleTickets.OrderByDescending(t => t.CreationDate).ToListAsync());
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
                if (ticketFilter.Equals("OlderFirst") || String.IsNullOrEmpty(ticketFilter))
                {
                    return View(await studentTroubleTickets.OrderBy(t => t.CreationDate).ToListAsync());
                }
                else
                {
                    return View(await studentTroubleTickets.OrderByDescending(t => t.CreationDate).ToListAsync());
                }
            }
            else if (User.IsInRole("Employee"))
            {
                var employee = _context.Employees.Where(e => e.Id.Equals(userId)).SingleOrDefault();
                employee.IsNotified = false;
                var troubleTickets = applicationDbContext.Where(t => t.ApplicationUserId == userId || String.IsNullOrEmpty(t.StudentNumber));
                if(String.IsNullOrEmpty(ticketFilter))
                {
                    return View(await troubleTickets.OrderByDescending(t => t.CreationDate).ToListAsync());
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
                if (ticketFilter.Equals("OlderFirst"))
                {
                    return View(await troubleTickets.OrderBy(t => t.CreationDate).ToListAsync());
                }
                else
                {
                    return View(await troubleTickets.OrderByDescending(t => t.CreationDate).ToListAsync());
                }
            }
            else
            {
                return View();
            }
        }

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
            var answersList = _context.TroubleTicketAnswers.Include(t => t.ApplicationUser).Include(t => t.TroubleTicket).Include(t => t.Document).Where(t => t.TroubleTicketId == id).ToList();
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

            loadHelp();
            return View();
        }

        // POST: TroubleTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TroubleTicketId,Title,Description,CreationDate,Solved,ApplicationUserId,StudentNumber")] TroubleTicket troubleTicket)
        {
            var student = _context.Students.Where(a => a.StudentNumber.Equals(troubleTicket.StudentNumber)).FirstOrDefault();
            if (User.IsInRole("Employee"))
            {
               
               if ( student == null)
                {
                    ViewData["ApplicationUserId"] = _context.ApplicationUsers.Where(a => a.Id == troubleTicket.ApplicationUserId).SingleOrDefault();
                    ViewData["ErrorMessage"] = "Não existe um estudante com esse número";
                    return View(troubleTicket);
                }
            }
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Employee"))
                {
                    student.IsNotified = true;
                }
                if (User.IsInRole("Student"))
                {
                    var employees = _context.Employees.ToList();
                    foreach(Employee e in employees)
                    {
                        e.IsNotified = true;
                    }
                }
                troubleTicket.Answers = new List<TroubleTicketAnswer>();
                _context.Add(troubleTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "TroubleTickets", new { id = troubleTicket.TroubleTicketId });
            }
            ViewData["ErrorMessage"] = "";
            ViewData["ApplicationUserId"] = _context.ApplicationUsers.Where(a => a.Id == troubleTicket.ApplicationUserId).SingleOrDefault();
            return View(troubleTicket);
        }

        
        // GET: TroubleTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troubleTicket = await _context.TroubleTicket.SingleOrDefaultAsync(m => m.TroubleTicketId == id);
            if (troubleTicket == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", troubleTicket.ApplicationUserId);
            return View(troubleTicket);
        }

        // POST: TroubleTickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TroubleTicketId,Title,Description,CreationDate,Solved,ApplicationUserId,StudentNumber")] TroubleTicket troubleTicket)
        {
            if (id != troubleTicket.TroubleTicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(troubleTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TroubleTicketExists(troubleTicket.TroubleTicketId))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", troubleTicket.ApplicationUserId);
            return View(troubleTicket);
        }

        // GET: TroubleTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troubleTicket = await _context.TroubleTicket
                .Include(t => t.ApplicationUser)
                .SingleOrDefaultAsync(m => m.TroubleTicketId == id);
            if (troubleTicket == null)
            {
                return NotFound();
            }

            return View(troubleTicket);
        }

        // POST: TroubleTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var troubleTicket = await _context.TroubleTicket.SingleOrDefaultAsync(m => m.TroubleTicketId == id);
            _context.TroubleTicket.Remove(troubleTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void loadHelp()
        {
            ViewData["TitleTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "TTTitle") as Help).HelpDescription;
            ViewData["DescriptionTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "TTDescription") as Help).HelpDescription;
            ViewData["StudentNumberTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "TTStudentNumber") as Help).HelpDescription;
        }

        private bool TroubleTicketExists(int id)
        {
            return _context.TroubleTicket.Any(e => e.TroubleTicketId == id);
        }
    }
}
