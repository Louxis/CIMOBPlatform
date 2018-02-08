using CIMOBProject.Data;
using CIMOBProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIMOBProject.ViewComponents
{
    public class UserFullNameViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public UserFullNameViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string id)
        {
            ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(u => u.Id.Equals(id));
            if(user != null)
            {
                ViewData["RandomUser"] = user.UserFullname;
            }
            else
            {
                ViewData["RandomUser"] = "wops";
            }
            
            return View();
        }
    }
}
