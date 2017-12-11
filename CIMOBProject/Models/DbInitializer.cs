using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using CIMOBProject.Data;
using Microsoft.AspNetCore.Builder;

namespace CIMOBProject.Models {
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            context.Database.EnsureCreated();            
            // Seed the database.
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

            if (!context.Students.Any())
            {
                var user = new Student
                {
                    UserName = "test@test",
                    UserFullname = "Teste User 1",
                    Email = "test@test",
                    UserCc = 123456789,
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123123123",
                    ALOGrade = 0,
                    CollegeSubjectId = 1
                };
                userManager.CreateAsync(user, "teste12").Wait();
                var role = context.Roles.SingleOrDefault(m => m.Name == "Student");
                userManager.AddToRoleAsync(user, role.Name).Wait();
                context.SaveChanges();
                context.Students.
                    SingleOrDefault(e => e.UserName.Equals("test@test")).EmailConfirmed = true;
                context.SaveChanges();
            }

            if (!context.Employees.Any())
            {
                var user = new Employee
                {
                    UserName = "testemployee@cimob.pt",
                    UserFullname = "Empregado Teste",
                    Email = "testemployee@cimob.pt",
                    UserCc = 123456789,
                    PhoneNumber = "936936936",
                    UserAddress = "RuaTeste",
                    PostalCode = "2900-000",
                    BirthDate = new DateTime(1996, 1, 1),
                    EmployeeNumber = 150221055
                };
                userManager.CreateAsync(user, "teste12").Wait();
                var role = context.Roles.SingleOrDefault(m => m.Name == "Employee");
                userManager.AddToRoleAsync(user, role.Name).Wait();
                context.SaveChanges();
                context.Employees
                    .SingleOrDefault(e => e.UserName == "testemployee@cimob.pt")
                    .EmailConfirmed = true;
                context.SaveChanges();
            }

            if (!context.Colleges.Any())
            {
                context.Colleges.Add(new College { CollegeAlias = "ESTS", CollegeName = "Escola Superior de Tecnologia de Setúbal" });
                context.Colleges.Add(new College { CollegeAlias = "ESCE", CollegeName = "Escola Superior de Ciências Empresariais" });
                context.Colleges.Add(new College { CollegeAlias = "ESE", CollegeName = "Escola Superior de Educação" });
                context.Colleges.Add(new College { CollegeAlias = "ESTB", CollegeName = "Escola Superior de Tecnologia do Barreiro" });
                context.SaveChanges();
            }

            if (!context.CollegeSubjects.Any())
            {
                context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "EI", SubjectName = "Engenharia Informática", CollegeId = 1 });
                context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "EM", SubjectName = "Engenharia Mecânica", CollegeId = 1 });
                context.SaveChanges();
            }
        }
    }




    
}
