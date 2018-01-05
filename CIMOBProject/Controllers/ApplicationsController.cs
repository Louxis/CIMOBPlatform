using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;
using System.Globalization;

namespace CIMOBProject.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index(String employeeId)
        {
            DateTime openDate = _context.Editals.Last().OpenDate;
            DateTime closeDate = _context.Editals.Last().CloseDate;
            var applicationDbContext = _context.Applications.Include(a => a.ApplicationStat)
                .Include(a => a.Employee).Include(a => a.Student).Include(a => a.BilateralProtocol1).Include(a => a.BilateralProtocol2).Include(a => a.BilateralProtocol3).Where(a => a.CreationDate >= openDate && a.CreationDate <= closeDate)
                .Where(a => a.EmployeeId.Equals(employeeId) || String.IsNullOrEmpty(a.EmployeeId));
            return View(await applicationDbContext.ToListAsync());
        }

        ///<summary>
        ///The objective of this method is to assign an employee to an application who will later evaluat it.
        ///</summary>
        public async Task<IActionResult> AssignEmployee(String employeeId, int applicationId)
        {
            var getAppliaction = _context.Applications.SingleOrDefault(a => a.ApplicationId == applicationId);

            getAppliaction.EmployeeId = employeeId;
            getAppliaction.ApplicationStatId = 2;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Applications", new { employeeId = employeeId });
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.ApplicationStat)
                .Include(a => a.Employee)
                .Include(a => a.Student)
                .SingleOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create(string userId)
        {
            var student = _context.Students.Include(s => s.CollegeSubject).Where(s => s.Id == userId).SingleOrDefault();
            if (DateTime.Now < _context.Editals.OrderBy(e => e.Id).SingleOrDefault().OpenDate)
            {
                return RedirectToAction("Application", "Home", new { message = "As candidaturas serão disponibilizadas no dia " + _context.Editals.OrderBy(e => e.Id).SingleOrDefault().OpenDate.ToString("MM/dd/yyyy") });
            }
            if (DateTime.Now > _context.Editals.OrderBy(e => e.Id).SingleOrDefault().CloseDate)
            {
                return RedirectToAction("Application", "Home", new { message = "Já terminou a data de entrega das candidaturas(" + _context.Editals.OrderBy(e => e.Id).SingleOrDefault().CloseDate.ToString("MM/dd/yyyy") + ") para o processo outgoing" });
            }
            ViewData["BilateralProtocol1Id"] = new SelectList(_context.BilateralProtocols.Where(p => p.SubjectId == student.CollegeSubjectId), "Id", "Destination");
            ViewData["BilateralProtocol2Id"] = new SelectList(_context.BilateralProtocols.Where(p => p.SubjectId == student.CollegeSubjectId), "Id", "Destination");
            ViewData["BilateralProtocol3Id"] = new SelectList(_context.BilateralProtocols.Where(p => p.SubjectId == student.CollegeSubjectId), "Id", "Destination");
            ViewData["StudentId"] = userId;
            ViewData["ApplicationStatId"] = 1;
            ViewData["EmployeeId"] = "";
            ViewData["CreationDate"] = DateTime.Now;
            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,StudentId,ApplicationStatId,EmployeeId,BilateralProtocol1Id,BilateralProtocol2Id,BilateralProtocol3Id,CreationDate,ArithmeticMean,ECTS,MotivationLetter,Enterview,FinalGrade")] Application application)
        {
            var student = _context.Students.Include(s => s.CollegeSubject).Where(s => s.Id == application.StudentId).SingleOrDefault();
            if (ModelState.IsValid)
            {
                //application.ApplicationStatId = 1;
                //application.EmployeeId = null;
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction("Application", "Home", new { message = "Candidatura efetuada com sucesso!" });
            }

            ViewData["BilateralProtocol1Id"] = new SelectList(_context.BilateralProtocols.Where(p => p.SubjectId == student.CollegeSubjectId), "Id", "Destination");
            ViewData["BilateralProtocol2Id"] = new SelectList(_context.BilateralProtocols.Where(p => p.SubjectId == student.CollegeSubjectId), "Id", "Destination");
            ViewData["BilateralProtocol3Id"] = new SelectList(_context.BilateralProtocols.Where(p => p.SubjectId == student.CollegeSubjectId), "Id", "Destination");
            ViewData["ApplicationStatId"] = 1;
            ViewData["EmployeeId"] = "";
            ViewData["CreationDate"] = DateTime.Now;
            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", application.StudentId);
            return RedirectToAction("Create", new { userId = application.StudentId });
        }
        ///<summary>
        ///The objective of this method is to display all the students who applied to the outgoing program with theyr respective grade.
        ///The only students who are displayed are those who had an application in the "Pending serialization" state.
        ///All the applications final grade is calculated in the moment of the seriation based on the the grades stored in each application.
        ///</summary>
        public async Task<IActionResult> Seriation()
        {
            
            DateTime openDate = _context.Editals.Last().OpenDate;
            DateTime closeDate = _context.Editals.Last().CloseDate;
            var queryGetApplication = await _context.Applications.Include(a => a.ApplicationStat).Include(a => a.Student).Include(a => a.Student.CollegeSubject).Include(a => a.Student.CollegeSubject.College).Include(a => a.BilateralProtocol1).Include(a => a.BilateralProtocol2).Include(a => a.BilateralProtocol3).Where(a => a.ApplicationStatId == 3 && a.CreationDate >= openDate && a.CreationDate <= closeDate).ToListAsync();

            foreach (var item in queryGetApplication)
            {
                item.FinalGrade = (item.MotivationLetter + item.Enterview + item.ArithmeticMean) / 3;
                await _context.SaveChangesAsync();
            }
            

            var OrderedList =  queryGetApplication.OrderByDescending(q => q.FinalGrade).ToList();
            foreach (var item in OrderedList)
            {
                if (item.BilateralProtocol1.OpenSlots > 0 && item.FinalGrade >= 9.5)
                {
                    item.ApplicationStatId = 4;
                    item.ApplicationStat = _context.ApplicationStats.SingleOrDefault(a => a.Id == 4);
                    item.BilateralProtocol1.OpenSlots -= 1;
                    item.BilateralProtocol2 = null;
                    item.BilateralProtocol3 = null;
                     _context.SaveChanges();
                }else if(item.BilateralProtocol2 != null && item.BilateralProtocol2.OpenSlots > 0 && item.FinalGrade >= 9.5)
                {
                    item.ApplicationStatId = 4;
                    item.ApplicationStat = _context.ApplicationStats.SingleOrDefault(a => a.Id == 4);
                    item.BilateralProtocol2.OpenSlots -= 1;
                    item.BilateralProtocol1 = null;
                    item.BilateralProtocol3 = null;
                    _context.SaveChanges();
                }else if(item.BilateralProtocol3 != null && item.BilateralProtocol3.OpenSlots > 0 && item.FinalGrade >= 9.5)
                {
                    item.ApplicationStatId = 4;
                    item.ApplicationStat = _context.ApplicationStats.SingleOrDefault(a => a.Id == 4);
                    item.BilateralProtocol3.OpenSlots -= 1;
                    item.BilateralProtocol1 = null;
                    item.BilateralProtocol2 = null;
                    _context.SaveChanges();
                }
                else
                {
                    item.ApplicationStatId = 5;
                    item.ApplicationStat = _context.ApplicationStats.SingleOrDefault(a => a.Id == 5);
                    _context.SaveChanges();
                }
            }

            return View(OrderedList);
        }
        ///<summary>
        ///The objective of this method is filter all applications by the ones the current logged in employee is evaluating or the ones that currently aren't being evaluated.
        ///</summary>
        public async Task<IActionResult> Filter(String filterType, String employeeId)
        {
            DateTime openDate = _context.Editals.Last().OpenDate;
            DateTime closeDate = _context.Editals.Last().CloseDate;
            var allApplications = _context.Applications.Include(a => a.ApplicationStat).Include(a => a.Employee).Include(a => a.Student).Where(a => a.CreationDate >= openDate && a.CreationDate <= closeDate);
            if (filterType.Equals("CurrentlySupervising"))
            {
                var filteredApplications = allApplications.Where(a => a.EmployeeId.Equals(employeeId));
                return View(await filteredApplications.ToListAsync());
            }
            if (filterType.Equals("NotSupervising"))
            {
                var filteredApplications = allApplications.Where(a => a.EmployeeId.Equals(null));
                return View(await filteredApplications.ToListAsync());
            }

            return View(await allApplications.ToListAsync());
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.Include(s => s.Student).SingleOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }
            //ViewData["BilateralProtocol1Id"] = new SelectList(_context.BilateralProtocols.Where(p => p.SubjectId == application.Student.CollegeSubjectId), "Id", "Destination");
            //ViewData["BilateralProtocol2Id"] = new SelectList(_context.BilateralProtocols.Where(p => p.SubjectId == application.Student.CollegeSubjectId), "Id", "Destination");
            //ViewData["BilateralProtocol3Id"] = new SelectList(_context.BilateralProtocols.Where(p => p.SubjectId == application.Student.CollegeSubjectId), "Id", "Destination");
            ViewData["BilateralProtocol1Id"] = application.BilateralProtocol1Id;
            ViewData["BilateralProtocol2Id"] = application.BilateralProtocol2Id;
            ViewData["BilateralProtocol3Id"] = application.BilateralProtocol3Id;
            ViewData["ApplicationStatId"] = new SelectList(_context.ApplicationStats.Where(a => a.Id != 1), "Id", "Name", application.ApplicationStatId);
            ViewData["EmployeeId"] = application.EmployeeId;
            ViewData["StudentId"] = application.StudentId;
            ViewData["CreationDate"] = DateTime.Now;
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,StudentId,ApplicationStatId,EmployeeId,BilateralProtocol1Id,BilateralProtocol2Id,BilateralProtocol3Id,CreationDate,ArithmeticMean,ECTS,MotivationLetter,Enterview,FinalGrade")] Application application)
        {

            if (id != application.ApplicationId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                
                try {
                    var getPreviousStat = _context.Applications.Include(a => a.ApplicationStat).SingleOrDefault(a => a.ApplicationId == id);
                    //var original = _context.Applications.Find(id);
                    //String getPreviousStat = _context.ApplicationStats.SingleOrDefault(a => a.Id == original.ApplicationStatId).Name;
                    _context.ApplicationStatHistory.Add(new ApplicationStatHistory { ApplicationId = id, ApplicationStat = getPreviousStat.ApplicationStat.Name, DateOfUpdate = DateTime.Now });
                    _context.Entry(getPreviousStat).State = EntityState.Detached;
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Applications", new { employeeId = application.EmployeeId });
            }
            
            ViewData["BilateralProtocol1Id"] = application.BilateralProtocol1Id;
            ViewData["BilateralProtocol2Id"] = application.BilateralProtocol2Id;
            ViewData["BilateralProtocol3Id"] = application.BilateralProtocol3Id;
            ViewData["EmployeeId"] = application.EmployeeId;
            ViewData["StudentId"] = application.StudentId;
            ViewData["CreationDate"] = DateTime.Now;
            return RedirectToAction("Index", "Applications", new { employeeId = application.EmployeeId });
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.ApplicationStat)
                .Include(a => a.Employee)
                .Include(a => a.Student)
                .SingleOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.SingleOrDefaultAsync(m => m.ApplicationId == id);
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ApplicationId == id);
        }
    }
}
