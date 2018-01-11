using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CIMOBProject.Models;
using System.Globalization;
using CIMOBProject.Data;

namespace CIMOBProject.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Publication()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Application(String message, String userId)
        {
            DateTime openDate = _context.Editals.OrderByDescending(e => e.Id).First().OpenDate;
            DateTime closeDate = _context.Editals.OrderByDescending(e => e.Id).First().CloseDate;

            if (DateTime.Now < openDate)
            { 
                message = "As candidaturas serão disponibilizadas no dia " + _context.Editals.OrderByDescending(e => e.Id).First().OpenDate.ToString("MM/dd/yyyy") + ".";
                return View((object)message);
            }
            if (DateTime.Now > closeDate)
            {
                message = "Já terminou a data de entrega das candidaturas (" + _context.Editals.OrderByDescending(e => e.Id).First().CloseDate.ToString("MM/dd/yyyy") + ") para o processo outgoing.";
                return View((object)message);
            }
            else
            {
                var query = _context.Applications.Where(s => s.StudentId.Equals(userId)).Where(a => a.CreationDate >= openDate && a.CreationDate <= closeDate);
                if (query.Any())
                {
                    return RedirectToAction("Details", "Applications", new { id = query.Last().ApplicationId });
                }
                message = "As candidaturas estão abertas!";
                return View((object)message);
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
