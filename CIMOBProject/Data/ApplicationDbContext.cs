﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<ApplicationStat> ApplicationStats { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Edital> Editals { get; set; }
        public DbSet<BilateralProtocol> BilateralProtocols { get; set; }
        public DbSet<Quizz> Quizzs { get; set; }
        public DbSet<TroubleTicket> TroubleTickets { get; set; }
        public DbSet<TroubleTicketAnswer> TroubleTicketAnswers { get; set; }
        public DbSet<Testemony> Testemonies { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<ApplicationStatHistory> ApplicationStatHistory { get; set; }
        public DbSet<TroubleTicket> TroubleTicket { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


    }
}
