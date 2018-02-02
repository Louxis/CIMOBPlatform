using CIMOBProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BackOfficeWPF {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext() : base(@"Server=(localdb)\mssqllocaldb;Database=CimobPlatform;Trusted_Connection=True;")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().Ignore(c => c.LockoutEndDateUtc);
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<CollegeSubject> CollegeSubjects { get; set; }
        public DbSet<ApplicationStat> ApplicationStats { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Edital> Editals { get; set; }
        public DbSet<BilateralProtocol> BilateralProtocols { get; set; }


        
    }
}
