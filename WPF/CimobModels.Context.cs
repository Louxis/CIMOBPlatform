﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CimobPlatformEntities1 : DbContext
    {
        public CimobPlatformEntities1()
            : base("name=CimobPlatformEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationStatHistory> ApplicationStatHistories { get; set; }
        public virtual DbSet<ApplicationStat> ApplicationStats { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<BilateralProtocol> BilateralProtocols { get; set; }
        public virtual DbSet<College> Colleges { get; set; }
        public virtual DbSet<CollegeSubject> CollegeSubjects { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<Help> Helps { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Quizz> Quizzs { get; set; }
    }
}
