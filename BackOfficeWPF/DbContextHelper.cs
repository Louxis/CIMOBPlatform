using CIMOBProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;

namespace BackOfficeWPF {
    class DbContextHelper {

        public static void AddEmployee(ApplicationDbContext context, Employee employee) {
            UserManager<Employee> userManager = new UserManager<Employee>(new UserStore<Employee>(new ApplicationDbContext()));
            try {
                var user = new Employee {
                    UserName = employee.Email,
                    UserFullname = employee.UserAddress,
                    Email = employee.Email,
                    UserCc = employee.UserCc,
                    PhoneNumber = employee.PhoneNumber,
                    UserAddress = employee.UserAddress,
                    PostalCode = employee.PostalCode,
                    BirthDate = employee.BirthDate,
                    EmployeeNumber = employee.EmployeeNumber,
                    NormalizedEmail = employee.Email.ToUpper(),
                    NormalizedUserName = employee.Email.ToUpper(),
                    EmailConfirmed = true
                };
                userManager.CreateAsync(user, "teste12").Wait();
                context.SaveChanges();
                var role = context.Roles.SingleOrDefault(m => m.Name == "Employee");
                userManager.AddToRoleAsync(user.Id, role.Name).Wait();
                context.SaveChanges();
                MessageBox.Show("Funcionário Criado com sucesso.", "Sucesso");
            } catch(Exception ex) {
                MessageBox.Show("Funcionário não criado, contactar suporte.", "Sucesso");
            }

            

            
        }

        public static void EditEmployee(ApplicationDbContext context, Employee employee) {
            var EmployeeToUpdate = context.Employees.Where(e => e.Id.Equals(employee.Id)).FirstOrDefault();
            EmployeeToUpdate.UserName = employee.Email;
            EmployeeToUpdate.UserFullname = employee.UserFullname;
            EmployeeToUpdate.Email = employee.Email;
            EmployeeToUpdate.UserCc = employee.UserCc;
            EmployeeToUpdate.PhoneNumber = employee.PhoneNumber;
            EmployeeToUpdate.UserAddress = employee.UserAddress;
            EmployeeToUpdate.PostalCode = employee.PostalCode;
            EmployeeToUpdate.BirthDate = employee.BirthDate;
            EmployeeToUpdate.EmployeeNumber = employee.EmployeeNumber;
            EmployeeToUpdate.NormalizedEmail = employee.Email.ToUpper();
            EmployeeToUpdate.NormalizedUserName = employee.Email.ToUpper();
            EmployeeToUpdate.IsBanned = employee.IsBanned;
            context.SaveChanges();
        }

        public static void EditStudent(ApplicationDbContext context, Student student) {
            var StudentToUpdate = context.Students.Where(e => e.Id.Equals(student.Id)).FirstOrDefault();
            StudentToUpdate.UserName = student.Email;
            StudentToUpdate.UserFullname = student.UserFullname;
            StudentToUpdate.Email = student.Email;
            StudentToUpdate.UserCc = student.UserCc;
            StudentToUpdate.PhoneNumber = student.PhoneNumber;
            StudentToUpdate.UserAddress = student.UserAddress;
            StudentToUpdate.PostalCode = student.PostalCode;
            StudentToUpdate.BirthDate = student.BirthDate;
            StudentToUpdate.StudentNumber = student.StudentNumber;
            StudentToUpdate.NormalizedEmail = student.Email.ToUpper();
            StudentToUpdate.NormalizedUserName = student.Email.ToUpper();
            StudentToUpdate.IsBanned = student.IsBanned;
            context.SaveChanges();
        }

        public static void EditApplication(ApplicationDbContext context, CIMOBProject.Models.Application application) {
            var applicationToUpdate = context.Applications.Where(a => a.ApplicationId == application.ApplicationId).FirstOrDefault();
            applicationToUpdate.ApplicationStatId = application.ApplicationStatId;
            context.SaveChanges();
        }

        public static void EditBilateral(ApplicationDbContext context, BilateralProtocol bilateralProtocol) {
            var bilateralToUpdate = context.BilateralProtocols.Where(b=> b.Id == bilateralProtocol.Id).FirstOrDefault();
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

        public static void AddBilateral(ApplicationDbContext context, BilateralProtocol bilateral) {
            try {
                context.BilateralProtocols.Add(bilateral);
                context.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show("Acordo não criado.", "Erro");
            }
            MessageBox.Show("Acordo bilateral criado com sucesso.", "Sucesso");
        }

        public static void AddCollege(ApplicationDbContext context, College college) {
            try {
                context.Colleges.Add(college);
                context.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show("Escola não criada.", "Erro");
            }
            MessageBox.Show("Escola criada com sucesso.", "Sucesso");
        }

        public static void AddCollegeSubject(ApplicationDbContext context, CollegeSubject subject) {
            try {
                context.CollegeSubjects.Add(subject);
                context.SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show("Curso não criado.", "Erro");
            }
            MessageBox.Show("Curso criado com sucesso.", "Sucesso");
        }       
    }
}
