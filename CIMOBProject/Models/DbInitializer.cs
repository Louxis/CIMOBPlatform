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
                context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "T1", SubjectName = "CursoTeste1", CollegeId = 2 });
                context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "T2", SubjectName = "CursoTeste2", CollegeId = 3 });
                context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "T3", SubjectName = "CursoTeste3", CollegeId = 3 });
                context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "T4", SubjectName = "CursoTeste4", CollegeId = 4 });
                context.SaveChanges();
            }

            if (!context.ApplicationStats.Any())
            {
                context.ApplicationStats.Add(new ApplicationStat { Name = "Pending Evaluation" });
                context.ApplicationStats.Add(new ApplicationStat { Name = "Evaluating" });
                context.ApplicationStats.Add(new ApplicationStat { Name = "Pending Serialization" });
                context.ApplicationStats.Add(new ApplicationStat { Name = "Approved" });
                context.ApplicationStats.Add(new ApplicationStat { Name = "Repproved" });
                context.ApplicationStats.Add(new ApplicationStat { Name = "Finished" });
                context.SaveChanges();
            }

            if (!context.BilateralProtocols.Any())
            {
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "Lithuania", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Spain", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "Testing1", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Testing2", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "Lithuania", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Testing4", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "Testing5", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Testing6", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "Testing7", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Testing8", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "Testing9", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Testing10", OpenSlots = 1 });
                context.SaveChanges();
            }

            if (!context.Students.Any())
            {
                var user1 = new Student
                {
                    UserName = "test@test",
                    UserFullname = "Teste User 1",
                    Email = "test@test",
                    UserCc = "12345678",
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123123123",
                    ALOGrade = 0,
                    CollegeSubjectId = 1
                };
                userManager.CreateAsync(user1, "teste12").Wait();
                var role = context.Roles.SingleOrDefault(m => m.Name == "Student");
                userManager.AddToRoleAsync(user1, role.Name).Wait();
                context.SaveChanges();
                context.Students.
                    SingleOrDefault(e => e.UserName.Equals("test@test")).EmailConfirmed = true;

                var user2 = new Student
                {
                    UserName = "test2@test",
                    UserFullname = "Teste User 2",
                    Email = "test2@test",
                    UserCc = "12345678",
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123123123",
                    ALOGrade = 0,
                    CollegeSubjectId = 1
                };
                userManager.CreateAsync(user2, "teste13").Wait();
                userManager.AddToRoleAsync(user2, role.Name).Wait();
                context.SaveChanges();
                context.Students.
                    SingleOrDefault(e => e.UserName.Equals("test2@test")).EmailConfirmed = true;

                var user3 = new Student
                {
                    UserName = "test3@test",
                    UserFullname = "Teste User 3",
                    Email = "test3@test",
                    UserCc = "12345678",
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123123123",
                    ALOGrade = 0,
                    CollegeSubjectId = 2
                };
                userManager.CreateAsync(user3, "teste14").Wait();
                userManager.AddToRoleAsync(user3, role.Name).Wait();
                context.SaveChanges();
                context.Students.
                    SingleOrDefault(e => e.UserName.Equals("test3@test")).EmailConfirmed = true;

                var user4 = new Student
                {
                    UserName = "test4@test",
                    UserFullname = "Teste User 4",
                    Email = "test4@test",
                    UserCc = "12345678",
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123123123",
                    ALOGrade = 0,
                    CollegeSubjectId = 2
                };
                userManager.CreateAsync(user4, "teste14").Wait();
                userManager.AddToRoleAsync(user4, role.Name).Wait();
                context.SaveChanges();
                context.Students.
                    SingleOrDefault(e => e.UserName.Equals("test4@test")).EmailConfirmed = true;

                var user5 = new Student
                {
                    UserName = "test5@test",
                    UserFullname = "Teste User 5",
                    Email = "test5@test",
                    UserCc = "12345678",
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123123123",
                    ALOGrade = 0,
                    CollegeSubjectId = 4
                };
                userManager.CreateAsync(user5, "teste15").Wait();
                userManager.AddToRoleAsync(user5, role.Name).Wait();
                context.SaveChanges();
                context.Students.
                    SingleOrDefault(e => e.UserName.Equals("test5@test")).EmailConfirmed = true;

                var user6 = new Student
                {
                    UserName = "test6@test",
                    UserFullname = "Teste User 6",
                    Email = "test6@test",
                    UserCc = "12345678",
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123123123",
                    ALOGrade = 0,
                    CollegeSubjectId = 4
                };
                userManager.CreateAsync(user6, "teste16").Wait();
                userManager.AddToRoleAsync(user6, role.Name).Wait();
                context.SaveChanges();
                context.Students.
                    SingleOrDefault(e => e.UserName.Equals("test6@test")).EmailConfirmed = true;

                var user7 = new Student
                {
                    UserName = "test7@test",
                    UserFullname = "Teste User 7",
                    Email = "test7@test",
                    UserCc = "12345678",
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123123123",
                    ALOGrade = 0,
                    CollegeSubjectId = 4
                };
                userManager.CreateAsync(user7, "teste17").Wait();
                userManager.AddToRoleAsync(user7, role.Name).Wait();
                context.SaveChanges();
                context.Students.
                    SingleOrDefault(e => e.UserName.Equals("test7@test")).EmailConfirmed = true;


                context.SaveChanges();
            }

            if (!context.Employees.Any())
            {
                var user = new Employee
                {
                    UserName = "testemployee@cimob.pt",
                    UserFullname = "Empregado Teste",
                    Email = "testemployee@cimob.pt",
                    UserCc = "12345689",
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

            if (!context.Applications.Any())
            {
                context.Applications.Add(new Application
                {
                    StudentId = context.Students.Where(s => s.UserFullname.Equals("Teste User 4")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 2,
                    BilateralProtocol2Id = 6,
                    BilateralProtocol3Id = 10,
                    EmployeeId = context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2017, 11, 11),
                    ArithmeticMean = 7.0,
                    ECTS = 78,
                    MotivationLetter = 8.0,
                    Enterview = 9.0,
                    FinalGrade = 8.0
                });
                context.Applications.Add(new Application
                {
                    StudentId = context.Students.Where(s => s.UserFullname.Equals("Teste User 2")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 1,
                    BilateralProtocol2Id = 5,
                    BilateralProtocol3Id = 9,
                    EmployeeId = context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2017, 11, 12),
                    ArithmeticMean = 10.0,
                    ECTS = 90,
                    MotivationLetter = 10.0,
                    Enterview = 10.0,
                    FinalGrade = 10.0
                });
               
                
                context.Applications.Add(new Application
                {
                    StudentId = context.Students.Where(s => s.UserFullname.Equals("Teste User 3")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 2,
                    BilateralProtocol2Id = 6,
                    BilateralProtocol3Id = 10,
                    //EmployeeId = context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2017, 11, 13),
                    ArithmeticMean = 8.0,
                    ECTS = 66,
                    MotivationLetter = 9.0,
                    Enterview = 7.0,
                    FinalGrade = 7.0
                });
                

                context.Applications.Add(new Application
                {
                    StudentId = context.Students.Where(s => s.UserFullname.Equals("Teste User 1")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 1,
                    BilateralProtocol2Id = 5,
                    BilateralProtocol3Id = 9,
                    EmployeeId = context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2017, 11, 14),
                    ArithmeticMean = 20.0,
                    ECTS = 120,
                    MotivationLetter = 20.0,
                    Enterview = 20.0,
                    FinalGrade = 20.0
                });

                context.Applications.Add(new Application
                {
                    StudentId = context.Students.Where(s => s.UserFullname.Equals("Teste User 7")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 4,
                    BilateralProtocol2Id = 8,
                    BilateralProtocol3Id = 12,
                    //EmployeeId = context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2017, 11, 15),
                    ArithmeticMean = 12.0,
                    ECTS = 120,
                    MotivationLetter = 15.0,
                    Enterview = 9.0,
                    FinalGrade = 11.0
                });

                context.Applications.Add(new Application
                {
                    StudentId = context.Students.Where(s => s.UserFullname.Equals("Teste User 5")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 4,
                    BilateralProtocol2Id = 8,
                    BilateralProtocol3Id = 12,
                    //EmployeeId = context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2017, 11, 16),
                    ArithmeticMean = 13.0,
                    ECTS = 120,
                    MotivationLetter = 8.0,
                    Enterview = 8.0,
                    FinalGrade = 9.0
                });

                context.Applications.Add(new Application
                {
                    StudentId = context.Students.Where(s => s.UserFullname.Equals("Teste User 6")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 1,
                    BilateralProtocol2Id = 5,
                    BilateralProtocol3Id = 9,
                    //EmployeeId = context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2016, 11, 10),
                    ArithmeticMean = 8.0,
                    ECTS = 120,
                    MotivationLetter = 9.0,
                    Enterview = 6.0,
                    FinalGrade = 7.0
                });
                context.SaveChanges();
            }

            if (!context.Editals.Any())
            {
                context.Editals.Add(new Edital {Title = "Edital teste", TextContent = "Edital publicado", OpenDate = new DateTime(2017,11,10), CloseDate = new DateTime(2018,01,09) });
                context.SaveChanges();
            }


            if (!context.Helps.Any())
            {
                context.Helps.Add(new Help { HelpDescription = "O nome deve ser constituido por vários caracteres que não incluam digitos nem caracteres especiais (*,+,_,etc.). Exe: Fernando Pessoa" });
                context.Helps.Add(new Help { HelpDescription = "O email seguir a estrutura valida dos emails. Exe: nomeExemplo@dominio.com." });
                context.Helps.Add(new Help { HelpDescription = "A password tem de ter um minimo de 6 caracteres. Exe: 123456." });
                context.Helps.Add(new Help { HelpDescription = "A password tem de ser identica à que foi introduzida anteriormente. Seguindo o exemplo anterior: 123456." });
                context.Helps.Add(new Help { HelpDescription = "O numero de telemóvel tem de ter 9 digitos. Exe: 960000000" });
                context.Helps.Add(new Help { HelpDescription = "Deve conter o nome da rua, o edificio e o andar. Exe: Avenida Dom Afonso Henriques nº 1" });
                context.Helps.Add(new Help { HelpDescription = "Deve seguir a estrutura dos códigos postais. Exe: 2000-100" });
                context.Helps.Add(new Help { HelpDescription = "A data de nascimento deve seguir a estrutura de mês, dia, ano. Exe: 1/13/1994" });
                context.Helps.Add(new Help { HelpDescription = "O CC deve ser constituido por 8 digitos." });
                context.Helps.Add(new Help { HelpDescription = "O número de estudante deve ser constituido por 9 digitos." });
                context.Helps.Add(new Help { HelpDescription = "Deve selecionar um curso da lista. Exe: EI" });
                context.Helps.Add(new Help { HelpDescription = "A Nota do Teste de línguas tem um valor de 0 a 20." });
                context.Helps.Add(new Help { HelpDescription = "O número de funcionário deve ser constituido por 9 digitos." });
                context.Helps.Add(new Help { HelpDescription = "A descrição deve descrever sumáriamente o documento." });
                context.Helps.Add(new Help { HelpDescription = "Insira o URL de onde está hospedado o seu documento." });
                context.SaveChanges();
            }
        }
    }    
}
