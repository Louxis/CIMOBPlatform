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
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Students.Include(s => s.CollegeSubject);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Search function to controll the searches that will go into the Students.Search View
        /// </summary>
        /// <param name="searchType">Type of search: Can be Student's number, name or college</param>
        /// <param name="searchString">Specification of what will be searched: name, number or college</param>
        /// <returns> A view with the search result listed</returns>
        // GET: Students
        [HttpGet]
        public async Task<IActionResult> Search(string searchType, string searchString) {

            var students = _context.Students.Include(s => s.CollegeSubject);

            if (String.IsNullOrEmpty(searchType) || String.IsNullOrEmpty(searchString))
            {
                return View("~/Views/Students/ErrorSearch.cshtml");
            }

            if (searchType.Equals("studentNumber") && !String.IsNullOrEmpty(searchString))
            {
                var filteredStudents = students.Where(s => s.StudentNumber.Contains(searchString));
                return View(await filteredStudents.ToListAsync());
            }
            else if (searchType.Equals("studentName") && !String.IsNullOrEmpty(searchString))
            {
                var filteredStudents = students.Where(s => s.UserFullname.Contains(searchString));
                return View(await filteredStudents.ToListAsync());
            }
            else if (searchType.Equals("studentCollege") && !String.IsNullOrEmpty(searchString))
            {
                var filteredStudents = students.Where(s => s.CollegeSubject.College.CollegeName.Contains(searchString));
                return View(await filteredStudents.ToListAsync());
            }
            else if (searchType.Equals("mail") && !String.IsNullOrEmpty(searchString))
            {
                var filteredStudents = students.Where(s => s.Email.Contains(searchString));
                //return Details(filteredStudents.FirstOrDefault().Id);

                return View(await filteredStudents.ToListAsync());
            }

            return View(await students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.CollegeSubject.College)
                .SingleOrDefaultAsync(m => m.Id == id);
            
            if (student == null)
            {
                return NotFound();
            }
            ViewData["selectedId"] = student.Id;
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["CollegeID"] = new SelectList(_context.Colleges, "Id", "CollegeName");
            ViewData["CollegeSubjectId"] = new SelectList(_context.CollegeSubjects, "Id", "SubjectName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentNumber,ALOGrade,CollegeID,UserFullname,PostalCode,BirthDate,UserAddress,UserCc,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollegeSubjectId"] = new SelectList(_context.CollegeSubjects, "Id", "SubjectName", student.CollegeSubjectId);
            return View(student);
        }




        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentNumber,ALOGrade,CollegeID,UserFullname,PostalCode,BirthDate,UserAddress,UserCc,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.CollegeSubject)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
