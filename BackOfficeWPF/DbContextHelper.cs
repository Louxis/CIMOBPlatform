using CIMOBProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Windows;

namespace BackOfficeWPF {
    /// <summary>
    /// This class will be used as a support for the creation of the different objects in the database.
    /// </summary>
    class DbContextHelper {

        public static void AddAdmin(ApplicationDbContext context, Employee applicationUser, string password) {
            UserManager<Employee> userManager = new UserManager<Employee>(new UserStore<Employee>(new ApplicationDbContext()));
            Employee user = null;
            user = new Employee {
                UserName = applicationUser.UserName,
                UserFullname = applicationUser.UserFullname,
                Email = applicationUser.Email,
                UserCc = applicationUser.UserCc,
                PhoneNumber = applicationUser.PhoneNumber,
                UserAddress = applicationUser.UserAddress,
                PostalCode = applicationUser.PostalCode,
                BirthDate = applicationUser.BirthDate,
                EmployeeNumber = applicationUser.EmployeeNumber,
                NormalizedEmail = applicationUser.UserName.ToUpper(),
                NormalizedUserName = applicationUser.UserName.ToUpper(),
                EmailConfirmed = true
            };
            userManager.CreateAsync(user, password).Wait();
            context.SaveChanges();
            var role = context.Roles.SingleOrDefault(m => m.Name == "Admin");
            userManager.AddToRoleAsync(user.Id, role.Name).Wait();
            context.SaveChanges();
            MessageBox.Show("Admin Criado com sucesso.", "Sucesso");
        }

        public static Employee AddEmployee(ApplicationDbContext context, Employee employee, string password) {
            UserManager<Employee> userManager = new UserManager<Employee>(new UserStore<Employee>(new ApplicationDbContext()));
            Employee user = null;
            try {
                user = new Employee {
                    UserName = employee.UserName,
                    UserFullname = employee.UserFullname,
                    Email = employee.UserName,
                    UserCc = employee.UserCc,
                    PhoneNumber = employee.PhoneNumber,
                    UserAddress = employee.UserAddress,
                    PostalCode = employee.PostalCode,
                    BirthDate = employee.BirthDate,
                    EmployeeNumber = employee.EmployeeNumber,
                    NormalizedEmail = employee.UserName.ToUpper(),
                    NormalizedUserName = employee.UserName.ToUpper(),
                    EmailConfirmed = true
                };
                userManager.CreateAsync(user, password).Wait();
                context.SaveChanges();
                var role = context.Roles.SingleOrDefault(m => m.Name == "Admin");
                userManager.AddToRoleAsync(user.Id, role.Name).Wait();
                context.SaveChanges();
                MessageBox.Show("Funcionário Criado com sucesso.", "Sucesso");
            }
            catch (Exception ex) {
                MessageBox.Show("Funcionário não criado, contactar suporte.", "Erro");
            }
            return user;
        }

        public static void EditEmployee(ApplicationDbContext context, Employee employee) {
            var EmployeeToUpdate = context.Employees.Where(e => e.Id.Equals(employee.Id)).FirstOrDefault();
            EmployeeToUpdate.UserName = employee.UserName;
            EmployeeToUpdate.UserFullname = employee.UserFullname;
            EmployeeToUpdate.Email = employee.UserName;
            EmployeeToUpdate.UserCc = employee.UserCc;
            EmployeeToUpdate.PhoneNumber = employee.PhoneNumber;
            EmployeeToUpdate.UserAddress = employee.UserAddress;
            EmployeeToUpdate.PostalCode = employee.PostalCode;
            EmployeeToUpdate.BirthDate = employee.BirthDate;
            EmployeeToUpdate.EmployeeNumber = employee.EmployeeNumber;
            EmployeeToUpdate.NormalizedEmail = employee.UserName.ToUpper();
            EmployeeToUpdate.NormalizedUserName = employee.UserName.ToUpper();
            EmployeeToUpdate.IsBanned = employee.IsBanned;
            context.SaveChanges();
        }

        public static void EditStudent(ApplicationDbContext context, Student student) {
            var StudentToUpdate = context.Students.Where(e => e.Id.Equals(student.Id)).FirstOrDefault();
            StudentToUpdate.UserName = student.UserName;
            StudentToUpdate.UserFullname = student.UserFullname;
            StudentToUpdate.Email = student.UserName;
            StudentToUpdate.UserCc = student.UserCc;
            StudentToUpdate.PhoneNumber = student.PhoneNumber;
            StudentToUpdate.UserAddress = student.UserAddress;
            StudentToUpdate.PostalCode = student.PostalCode;
            StudentToUpdate.BirthDate = student.BirthDate;
            StudentToUpdate.StudentNumber = student.StudentNumber;
            StudentToUpdate.NormalizedEmail = student.UserName.ToUpper();
            StudentToUpdate.NormalizedUserName = student.UserName.ToUpper();
            StudentToUpdate.IsBanned = student.IsBanned;
            context.SaveChanges();
        }

        public static void EditApplication(ApplicationDbContext context, CIMOBProject.Models.Application application) {
            var applicationToUpdate = context.Applications.Where(a => a.ApplicationId == application.ApplicationId).FirstOrDefault();
            applicationToUpdate.ApplicationStatId = application.ApplicationStatId;
            applicationToUpdate.ApplicationStat = application.ApplicationStat;
            context.SaveChanges();
        }

        public static void EditBilateral(ApplicationDbContext context, BilateralProtocol bilateralProtocol) {
            var bilateralToUpdate = context.BilateralProtocols.Where(b => b.Id == bilateralProtocol.Id).FirstOrDefault();
            bilateralToUpdate.Destination = bilateralProtocol.Destination;
            bilateralToUpdate.OpenSlots = bilateralProtocol.OpenSlots;
            bilateralToUpdate.SubjectId = bilateralProtocol.SubjectId;
            context.SaveChanges();
        }

        public static void EditCollege(ApplicationDbContext context, College college) {
            var collegeToUpdate = context.Colleges.Where(c => c.Id == college.Id).FirstOrDefault();
            collegeToUpdate.CollegeName = college.CollegeName;
            collegeToUpdate.CollegeAlias = college.CollegeAlias;
            context.SaveChanges();
        }

        internal static void EditSubject(ApplicationDbContext context, CollegeSubject collegeSubject) {
            var collegeSubjectToUpdate = context.CollegeSubjects.Where(c => c.Id == collegeSubject.Id).FirstOrDefault();
            collegeSubjectToUpdate.SubjectName = collegeSubject.SubjectName;
            collegeSubjectToUpdate.SubjectAlias = collegeSubject.SubjectAlias;
            collegeSubjectToUpdate.CollegeId = collegeSubject.CollegeId;
            context.SaveChanges();
        }

        public static BilateralProtocol AddBilateral(ApplicationDbContext context, BilateralProtocol bilateral) {
            try {
                context.BilateralProtocols.Add(bilateral);
                context.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show("Acordo não criado.", "Erro");
            }
            MessageBox.Show("Acordo bilateral criado com sucesso.", "Sucesso");
            return bilateral;
        }

        public static College AddCollege(ApplicationDbContext context, College college) {
            try {
                context.Colleges.Add(college);
                context.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show("Escola não criada.", "Erro");
            }
            MessageBox.Show("Escola criada com sucesso.", "Sucesso");
            return college;
        }

        public static CollegeSubject AddCollegeSubject(ApplicationDbContext context, CollegeSubject subject) {
            try {
                context.CollegeSubjects.Add(subject);
                context.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show("Curso não criado.", "Erro");
            }
            MessageBox.Show("Curso criado com sucesso.", "Sucesso");
            return subject;
        }
    }
}
