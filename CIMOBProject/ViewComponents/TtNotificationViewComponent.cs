using CIMOBProject.Data;
using CIMOBProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIMOBProject.ViewComponents
{
    public class TtNotificationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public TtNotificationViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string id)
        {
            ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(u => u.Id.Equals(id));
            //Using a true false system, can convert this into img src
            if (user != null)
            {
                if (user.IsNotified)
                {
                    //image for notification
                    ViewData["Notified"] = true;
                }
                else
                {
                    ViewData["Notified"] = false;
                }                
            }
            return View();
        }
    }
}
