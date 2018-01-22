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
                context.ApplicationStats.Add(new ApplicationStat { Name = "Avaliação Pendente" });
                context.ApplicationStats.Add(new ApplicationStat { Name = "Em Avaliação" });
                context.ApplicationStats.Add(new ApplicationStat { Name = "Seriação Pendente" });
                context.ApplicationStats.Add(new ApplicationStat { Name = "Aprovado" });
                context.ApplicationStats.Add(new ApplicationStat { Name = "Reprovado" });
                context.ApplicationStats.Add(new ApplicationStat { Name = "Finalizado" });
                context.SaveChanges();
            }

            if (!context.BilateralProtocols.Any())
            {
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "França", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Espanha", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "Italia", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Romenia", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "Lituania", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Polónia", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "Russia", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Londres", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "Irlanda", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Turquia", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "França", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Alemanha", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "França", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Espanha", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "Italia", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Romenia", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "Lituania", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Polónia", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "Russia", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Londres", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "Irlanda", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Turquia", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "França", OpenSlots = 1 });
                context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Alemanha", OpenSlots = 1 });
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
                    StudentNumber = "123456789",
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
                    StudentNumber = "123795884",
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
                    StudentNumber = "123244144",
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
                    StudentNumber = "123333333",
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
                    StudentNumber = "123111111",
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
                    StudentNumber = "123123133",
                    ALOGrade = 0,
                    CollegeSubjectId = 4
                };
                userManager.CreateAsync(user7, "teste17").Wait();
                userManager.AddToRoleAsync(user7, role.Name).Wait();
                context.SaveChanges();
                context.Students.
                    SingleOrDefault(e => e.UserName.Equals("test7@test")).EmailConfirmed = true;

                var user8 = new Student
                {
                    UserName = "test8@test",
                    UserFullname = "Teste User 8",
                    Email = "test8@test",
                    UserCc = "12345678",
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123122222",
                    ALOGrade = 0,
                    CollegeSubjectId = 4
                };
                userManager.CreateAsync(user8, "teste18").Wait();
                userManager.AddToRoleAsync(user8, role.Name).Wait();
                context.SaveChanges();
                context.Students.
                    SingleOrDefault(e => e.UserName.Equals("test8@test")).EmailConfirmed = true;

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
                    EmployeeNumber = "150221055"
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
                /*context.Applications.Add(new Application
                {
                    StudentId = context.Students.Where(s => s.UserFullname.Equals("Teste User 4")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 2,
                    BilateralProtocol2Id = 6,
                    BilateralProtocol3Id = 10,
                    //EmployeeId = context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2017, 11, 11),
                    ArithmeticMean = 7.0,
                    ECTS = 78,
                    MotivationLetter = 8.0,
                    Enterview = 9.0,
                    FinalGrade = 8.0
                });*/
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
                

                /*context.Applications.Add(new Application
                {
                    StudentId = context.Students.Where(s => s.UserFullname.Equals("Teste User 1")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 1,
                    BilateralProtocol2Id = 5,
                    BilateralProtocol3Id = 9,
                    EmployeeId = context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2018, 01, 03),
                    ArithmeticMean = 20.0,
                    ECTS = 120,
                    MotivationLetter = 20.0,
                    Enterview = 20.0,
                    FinalGrade = 20.0
                });*/

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
                context.Editals.Add(new Edital {
                    Title = "Edital teste",
                    TextContent = "Edital publicado",
                    OpenDate = new DateTime(2017,11,10),
                    CloseDate = new DateTime(2018,02,03),
                    IsPublished = true });
                context.SaveChanges();
            }


            if (!context.Helps.Any())
            {
                context.Helps.Add(new Help { HelpName = "UserName", HelpDescription = "O nome deve ser constituido por vários caracteres que não incluam digitos nem caracteres especiais (*,+,_,etc.). Exe: Fernando Pessoa" });
                context.Helps.Add(new Help { HelpName = "Email", HelpDescription = "O email seguir a estrutura valida dos emails. Exe: nomeExemplo@dominio.com." });
                context.Helps.Add(new Help { HelpName = "Password", HelpDescription = "A password tem de ter um minimo de 6 caracteres. Exe: 123456." });
                context.Helps.Add(new Help { HelpName = "ConfirmPassword", HelpDescription = "A password tem de ser identica à que foi introduzida anteriormente. Seguindo o exemplo anterior: 123456." });
                context.Helps.Add(new Help { HelpName = "PhoneNumber", HelpDescription = "O numero de telemóvel tem de ter 9 digitos. Exe: 960000000" });
                context.Helps.Add(new Help { HelpName = "UserAddress", HelpDescription = "Deve conter o nome da rua, o edificio e o andar. Exe: Avenida Dom Afonso Henriques nº 1" });
                context.Helps.Add(new Help { HelpName = "PostalCode", HelpDescription = "Deve seguir a estrutura dos códigos postais. Exe: 2000-100" });
                context.Helps.Add(new Help { HelpName = "BirthDate", HelpDescription = "A data de nascimento deve seguir a estrutura de mês, dia, ano. Exe: 1/13/1994" });
                context.Helps.Add(new Help { HelpName = "UserCc", HelpDescription = "O CC deve ser constituido por 8 digitos." });
                context.Helps.Add(new Help { HelpName = "StudentNumber", HelpDescription = "O número de estudante deve ser constituido por 9 digitos." });
                context.Helps.Add(new Help { HelpName = "CollegeSubject", HelpDescription = "Deve selecionar um curso da lista. Exe: EI" });
                context.Helps.Add(new Help { HelpName = "ALOGrade", HelpDescription = "A Nota do Teste de línguas tem um valor de 0 a 20." });
                context.Helps.Add(new Help { HelpName = "EmployeeNumber", HelpDescription = "O número de funcionário deve ser constituido por 9 digitos." });
                context.Helps.Add(new Help { HelpName = "Description", HelpDescription = "A descrição deve descrever sumáriamente o documento." });
                context.Helps.Add(new Help { HelpName = "FileURL", HelpDescription = "Insira o URL de onde está hospedado o seu documento." });
                context.Helps.Add(new Help { HelpName = "CurrentPassword", HelpDescription = "Insira a sua password atual" });
                context.Helps.Add(new Help { HelpName = "Bilateral", HelpDescription = "Escolha o destino que deseja." });
                context.Helps.Add(new Help { HelpName = "MotivationLetter", HelpDescription = "Escreva a razão pela qual deseja ingressar no programa Erasmus." });
                context.Helps.Add(new Help { HelpName = "Grade", HelpDescription = "Insira a nota aritmética do estudante, no valor de 0 a 20." });
                context.Helps.Add(new Help { HelpName = "MotivationGrade", HelpDescription = "Insira a nota da motivação do estudante, no valor de 0 a 20." });
                context.Helps.Add(new Help { HelpName = "Interview", HelpDescription = "Insira a nota da entrevista do estudante, no valor de 0 a 20." });
                context.Helps.Add(new Help { HelpName = "Title", HelpDescription = "Insira o título que deseja dar à sua publicação." });
                context.Helps.Add(new Help { HelpName = "TextContent", HelpDescription = "Insira o conteúdo da publicação." });
                context.Helps.Add(new Help { HelpName = "OpenDate", HelpDescription = "Insira a data de início da fase de candidaturas." });
                context.Helps.Add(new Help { HelpName = "CloseDate", HelpDescription = "Insira a data de encerramento da fase de candidaturas." });
                context.Helps.Add(new Help { HelpName = "Year", HelpDescription = "Insira o ano a que se refere o questionário." });
                context.Helps.Add(new Help { HelpName = "Semester", HelpDescription = "Insira o semestre a que se refere o questionário." });
                context.Helps.Add(new Help { HelpName = "QuizURL", HelpDescription = "Insira o URL de onde está hospedado o questionário." });
                context.Helps.Add(new Help { HelpName = "InterviewDate", HelpDescription = "Insira a data na qual pretende efetuar a entrevista ao aluno." });
                context.Helps.Add(new Help { HelpName = "TTDescription", HelpDescription = "Descreva detalhadamente o seu problema." });
                context.Helps.Add(new Help { HelpName = "TTTitle", HelpDescription = "Insira o título que deseja dar ao seu Trouble Ticket." });
                context.Helps.Add(new Help { HelpName = "TTStudentNumber", HelpDescription = "Insira número de aluno que pretende contactar." });
                context.SaveChanges();
            }
        }
    }    
}
