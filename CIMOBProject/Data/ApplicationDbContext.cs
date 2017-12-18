using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Models;

namespace CIMOBProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        ///<summary>
        ///The main objective of this class can be described as beeing the bridge between the application and it's data base.
        ///This class is the one that will handle the communication with the DB meaing it will handle all the CRUDs.
        ///It also takes care of relationships between the tables.
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<CollegeSubject> CollegeSubjects { get; set; }
        public DbSet<Help> Helps { get; set; }
        public DbSet<Error> Errors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
