﻿// <auto-generated />
using CIMOBProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace CIMOBProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180120103353_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CIMOBProject.Models.Application", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApplicationStatId");

                    b.Property<double?>("ArithmeticMean");

                    b.Property<int?>("BilateralProtocol1Id");

                    b.Property<int?>("BilateralProtocol2Id");

                    b.Property<int?>("BilateralProtocol3Id");

                    b.Property<DateTime>("CreationDate");

                    b.Property<int?>("ECTS");

                    b.Property<string>("EmployeeId");

                    b.Property<double?>("Enterview");

                    b.Property<double?>("FinalGrade");

                    b.Property<double?>("MotivationLetter");

                    b.Property<string>("Motivations");

                    b.Property<string>("StudentId");

                    b.HasKey("ApplicationId");

                    b.HasIndex("ApplicationStatId");

                    b.HasIndex("BilateralProtocol1Id");

                    b.HasIndex("BilateralProtocol2Id");

                    b.HasIndex("BilateralProtocol3Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("StudentId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("CIMOBProject.Models.ApplicationStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ApplicationStats");
                });

            modelBuilder.Entity("CIMOBProject.Models.ApplicationStatHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApplicationId");

                    b.Property<string>("ApplicationStat");

                    b.Property<DateTime>("DateOfUpdate");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("ApplicationStatHistory");
                });

            modelBuilder.Entity("CIMOBProject.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsNotified");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PostalCode")
                        .IsRequired();

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserAddress")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("UserCc")
                        .IsRequired();

                    b.Property<string>("UserFullname")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");
                });

            modelBuilder.Entity("CIMOBProject.Models.BilateralProtocol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Destination")
                        .IsRequired();

                    b.Property<int>("OpenSlots");

                    b.Property<int>("SubjectId");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("BilateralProtocols");
                });

            modelBuilder.Entity("CIMOBProject.Models.College", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CollegeAlias")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("CollegeName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Colleges");
                });

            modelBuilder.Entity("CIMOBProject.Models.CollegeSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CollegeId");

                    b.Property<string>("SubjectAlias")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CollegeId");

                    b.ToTable("CollegeSubjects");
                });

            modelBuilder.Entity("CIMOBProject.Models.Document", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApplicationId");

                    b.Property<string>("Description");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("FileUrl")
                        .IsRequired();

                    b.Property<DateTime>("UploadDate");

                    b.HasKey("DocumentId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("CIMOBProject.Models.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ErrorCode")
                        .IsRequired();

                    b.Property<int>("ErrorDescription");

                    b.HasKey("Id");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("CIMOBProject.Models.Help", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HelpDescription")
                        .IsRequired();

                    b.Property<string>("HelpName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Helps");
                });

            modelBuilder.Entity("CIMOBProject.Models.Interview", b =>
                {
                    b.Property<int>("InterviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApplicationId");

                    b.Property<string>("EmployeeId");

                    b.Property<DateTime>("InterviewDate");

                    b.HasKey("InterviewId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Interviews");
                });

            modelBuilder.Entity("CIMOBProject.Models.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int?>("DocumentId");

                    b.Property<string>("EmployeeId");

                    b.Property<bool>("IsPublished");

                    b.Property<string>("TextContent")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("News");

                    b.HasDiscriminator<string>("Discriminator").HasValue("News");
                });

            modelBuilder.Entity("CIMOBProject.Models.Quizz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsPublished");

                    b.Property<string>("QuizzUrl")
                        .IsRequired();

                    b.Property<int>("Semester");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Quizzs");
                });

            modelBuilder.Entity("CIMOBProject.Models.Testemony", b =>
                {
                    b.Property<int>("TestemonyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("StudentId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<bool>("Valid");

                    b.HasKey("TestemonyId");

                    b.HasIndex("StudentId");

                    b.ToTable("Testemonies");
                });

            modelBuilder.Entity("CIMOBProject.Models.TroubleTicket", b =>
                {
                    b.Property<int>("TroubleTicketId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("Solved");

                    b.Property<string>("StudentNumber");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("TroubleTicketId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("TroubleTicket");
                });

            modelBuilder.Entity("CIMOBProject.Models.TroubleTicketAnswer", b =>
                {
                    b.Property<int>("TroubleTicketAnswerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("TroubleTicketId");

                    b.HasKey("TroubleTicketAnswerId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("TroubleTicketId");

                    b.ToTable("TroubleTicketAnswers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CIMOBProject.Models.Employee", b =>
                {
                    b.HasBaseType("CIMOBProject.Models.ApplicationUser");

                    b.Property<int>("EmployeeNumber");

                    b.ToTable("Employee");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("CIMOBProject.Models.Student", b =>
                {
                    b.HasBaseType("CIMOBProject.Models.ApplicationUser");

                    b.Property<int>("ALOGrade");

                    b.Property<int?>("CollegeId");

                    b.Property<int>("CollegeSubjectId");

                    b.Property<string>("StudentNumber")
                        .IsRequired();

                    b.HasIndex("CollegeId");

                    b.HasIndex("CollegeSubjectId");

                    b.ToTable("Student");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("CIMOBProject.Models.Edital", b =>
                {
                    b.HasBaseType("CIMOBProject.Models.News");

                    b.Property<DateTime>("CloseDate");

                    b.Property<DateTime>("OpenDate");

                    b.ToTable("Edital");

                    b.HasDiscriminator().HasValue("Edital");
                });

            modelBuilder.Entity("CIMOBProject.Models.Application", b =>
                {
                    b.HasOne("CIMOBProject.Models.ApplicationStat", "ApplicationStat")
                        .WithMany()
                        .HasForeignKey("ApplicationStatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIMOBProject.Models.BilateralProtocol", "BilateralProtocol1")
                        .WithMany()
                        .HasForeignKey("BilateralProtocol1Id");

                    b.HasOne("CIMOBProject.Models.BilateralProtocol", "BilateralProtocol2")
                        .WithMany()
                        .HasForeignKey("BilateralProtocol2Id");

                    b.HasOne("CIMOBProject.Models.BilateralProtocol", "BilateralProtocol3")
                        .WithMany()
                        .HasForeignKey("BilateralProtocol3Id");

                    b.HasOne("CIMOBProject.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("CIMOBProject.Models.Student", "Student")
                        .WithMany("Applications")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("CIMOBProject.Models.ApplicationStatHistory", b =>
                {
                    b.HasOne("CIMOBProject.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIMOBProject.Models.BilateralProtocol", b =>
                {
                    b.HasOne("CIMOBProject.Models.CollegeSubject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIMOBProject.Models.CollegeSubject", b =>
                {
                    b.HasOne("CIMOBProject.Models.College", "College")
                        .WithMany("Subjects")
                        .HasForeignKey("CollegeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIMOBProject.Models.Document", b =>
                {
                    b.HasOne("CIMOBProject.Models.Application", "Application")
                        .WithMany("Documents")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("CIMOBProject.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("CIMOBProject.Models.Interview", b =>
                {
                    b.HasOne("CIMOBProject.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIMOBProject.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("CIMOBProject.Models.News", b =>
                {
                    b.HasOne("CIMOBProject.Models.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId");

                    b.HasOne("CIMOBProject.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("CIMOBProject.Models.Testemony", b =>
                {
                    b.HasOne("CIMOBProject.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("CIMOBProject.Models.TroubleTicket", b =>
                {
                    b.HasOne("CIMOBProject.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("CIMOBProject.Models.TroubleTicketAnswer", b =>
                {
                    b.HasOne("CIMOBProject.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("CIMOBProject.Models.TroubleTicket", "TroubleTicket")
                        .WithMany("Answers")
                        .HasForeignKey("TroubleTicketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CIMOBProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CIMOBProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CIMOBProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CIMOBProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CIMOBProject.Models.Student", b =>
                {
                    b.HasOne("CIMOBProject.Models.College")
                        .WithMany("Students")
                        .HasForeignKey("CollegeId");

                    b.HasOne("CIMOBProject.Models.CollegeSubject", "CollegeSubject")
                        .WithMany()
                        .HasForeignKey("CollegeSubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
