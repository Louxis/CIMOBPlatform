using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using CIMOBProject.Data;

namespace CIMOBProject.Models {
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            Boolean _adicionou = false;
            context.Database.EnsureCreated();

            if (context.Roles.SingleOrDefault(r => r.Name == "Student") == null)
            {
                
                context.Roles.Add(new IdentityRole { Name = "Student", NormalizedName = "Student" });
                context.SaveChanges();
            }

            if (context.Roles.SingleOrDefault(r => r.Name == "Employee") == null)
            {
                context.Roles.Add(new IdentityRole { Name = "Employee", NormalizedName = "Employee" });
                context.SaveChanges();
            }
        }
    }




    
}
