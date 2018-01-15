using System;
using System.Collections.Generic;
using System.Data.Entity;
using CIMOBProject.Controllers;
using CIMOBProject.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF {
    class ApplicationDbContext : DbContext {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<CollegeSubject> CollegeSubjects { get; set; }
        public DbSet<Help> Helps { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<ApplicationStat> ApplicationStats { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Edital> Editals { get; set; }
        public DbSet<BilateralProtocol> BilateralProtocols { get; set; }
        public DbSet<Quizz> Quizzs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<CIMOBProject.Models.ApplicationStatHistory> ApplicationStatHistory { get; set; }

        public ApplicationDbContext() : base(@"Server=(localdb)\mssqllocaldb;Database=PVLab10;Trusted_Connection=True;")
        { }
    }
}
