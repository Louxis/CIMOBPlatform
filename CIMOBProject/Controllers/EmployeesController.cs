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
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeNumber,UserFullname,PostalCode,BirthDate,UserAddress,UserCc,PhoneNumber,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            loadHelp();
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmployeeNumber,UserFullname,PostalCode,BirthDate,UserAddress,UserCc,PhoneNumber,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }
            var toEditEmployee = await _context.Employees
                .SingleOrDefaultAsync(m => m.Id == id);

            toEditEmployee.EmployeeNumber = employee.EmployeeNumber;
            toEditEmployee.UserAddress = employee.UserAddress;
            toEditEmployee.PostalCode = employee.PostalCode;
            toEditEmployee.PhoneNumber = employee.PhoneNumber;
            toEditEmployee.UserCc = employee.UserCc;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toEditEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Details", "Employees", new { id = toEditEmployee.Id });
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.Id == id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        private void loadHelp()
        {
            ViewData["UserNameTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 1) as Help).HelpDescription;
            ViewData["BirthDateTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 8) as Help).HelpDescription;
            ViewData["UserCcTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 9) as Help).HelpDescription;
            ViewData["PhoneNumberTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 5) as Help).HelpDescription;
            ViewData["UserAddressTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 6) as Help).HelpDescription;
            ViewData["PostalCodeTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 7) as Help).HelpDescription;
            ViewData["EmployeeNumberTip"] = (_context.Helps.FirstOrDefault(h => h.Id == 13) as Help).HelpDescription;
        }
    }
}
