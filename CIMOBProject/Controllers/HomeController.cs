using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CIMOBProject.Models;
using System.Globalization;

namespace CIMOBProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Publication()
        {
            return View();
        }

        public IActionResult Application(String message)
        {
            if(message == null)
            {
                message = "Necissita estar logged in para ver informações relativamente a candidatura";
                return View((object)message);
            }

            return View((object) message);
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
