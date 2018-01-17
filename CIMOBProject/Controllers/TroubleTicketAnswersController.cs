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
    public class TroubleTicketAnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TroubleTicketAnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TroubleTicketAnswers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TroubleTicketAnswers.Include(t => t.ApplicationUser).Include(t => t.TroubleTicket);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TroubleTicketAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troubleTicketAnswer = await _context.TroubleTicketAnswers
                .Include(t => t.ApplicationUser)
                .Include(t => t.TroubleTicket)
                .SingleOrDefaultAsync(m => m.TroubleTicketAnswerId == id);
            if (troubleTicketAnswer == null)
            {
                return NotFound();
            }

            return View(troubleTicketAnswer);
        }

        // GET: TroubleTicketAnswers/Create
        public IActionResult Create(string applicationUserId, int troubleTicketId)
        {
            ViewData["TroubleTicketId"] = troubleTicketId;
            ViewData["ApplicationUserId"] = applicationUserId;
            ViewData["CreationDate"] = DateTime.Now;
            return View();
        }

        // POST: TroubleTicketAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TroubleTicketAnswerId,Content,TroubleTicketId,ApplicationUserId,CreationDate")] TroubleTicketAnswer troubleTicketAnswer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(troubleTicketAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "TroubleTickets", new { userId = troubleTicketAnswer.ApplicationUserId });
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", troubleTicketAnswer.ApplicationUserId);
            ViewData["TroubleTicketId"] = new SelectList(_context.TroubleTickets, "TroubleTicketId", "TroubleTicketId", troubleTicketAnswer.TroubleTicketId);
            return View(troubleTicketAnswer);
        }

        // GET: TroubleTicketAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troubleTicketAnswer = await _context.TroubleTicketAnswers.SingleOrDefaultAsync(m => m.TroubleTicketAnswerId == id);
            if (troubleTicketAnswer == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", troubleTicketAnswer.ApplicationUserId);
            ViewData["TroubleTicketId"] = new SelectList(_context.TroubleTickets, "TroubleTicketId", "TroubleTicketId", troubleTicketAnswer.TroubleTicketId);
            return View(troubleTicketAnswer);
        }

        // POST: TroubleTicketAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TroubleTicketAnswerId,Content,TroubleTicketId,ApplicationUserId,CreationDate")] TroubleTicketAnswer troubleTicketAnswer)
        {
            if (id != troubleTicketAnswer.TroubleTicketAnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(troubleTicketAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TroubleTicketAnswerExists(troubleTicketAnswer.TroubleTicketAnswerId))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", troubleTicketAnswer.ApplicationUserId);
            ViewData["TroubleTicketId"] = new SelectList(_context.TroubleTickets, "TroubleTicketId", "TroubleTicketId", troubleTicketAnswer.TroubleTicketId);
            return View(troubleTicketAnswer);
        }

        // GET: TroubleTicketAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troubleTicketAnswer = await _context.TroubleTicketAnswers
                .Include(t => t.ApplicationUser)
                .Include(t => t.TroubleTicket)
                .SingleOrDefaultAsync(m => m.TroubleTicketAnswerId == id);
            if (troubleTicketAnswer == null)
            {
                return NotFound();
            }

            return View(troubleTicketAnswer);
        }

        // POST: TroubleTicketAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var troubleTicketAnswer = await _context.TroubleTicketAnswers.SingleOrDefaultAsync(m => m.TroubleTicketAnswerId == id);
            _context.TroubleTicketAnswers.Remove(troubleTicketAnswer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TroubleTicketAnswerExists(int id)
        {
            return _context.TroubleTicketAnswers.Any(e => e.TroubleTicketAnswerId == id);
        }
    }
}
