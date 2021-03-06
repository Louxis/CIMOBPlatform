﻿using CIMOBProject.Controllers;
using CIMOBProject.Data;
using CIMOBProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTesting
{
    public class ApplicationControllerTestXUnit
    {
        private IConfigurationRoot _configuration;

        // represents database's configuration
        private DbContextOptions<ApplicationDbContext> _options;
        private ApplicationDbContext _context;

        public ApplicationControllerTestXUnit()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .Options;
            InitializeDatabaseWithDataTest();
        }

        internal void InitializeDatabaseWithDataTest()
        {
            _context = new ApplicationDbContext(_options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            if (!_context.Colleges.Any())
            {
                _context.Colleges.Add(new College { CollegeAlias = "ESTS", CollegeName = "Escola Superior de Tecnologia de Setúbal" });
                _context.Colleges.Add(new College { CollegeAlias = "ESCE", CollegeName = "Escola Superior de Ciências Empresariais" });
                _context.Colleges.Add(new College { CollegeAlias = "ESE", CollegeName = "Escola Superior de Educação" });
                _context.Colleges.Add(new College { CollegeAlias = "ESTB", CollegeName = "Escola Superior de Tecnologia do Barreiro" });
                _context.SaveChanges();
            }

            if (!_context.CollegeSubjects.Any())
            {
                _context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "EI", SubjectName = "Engenharia Informática", CollegeId = 1 });
                _context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "EM", SubjectName = "Engenharia Mecânica", CollegeId = 1 });
                _context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "T1", SubjectName = "CursoTeste1", CollegeId = 2 });
                _context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "T2", SubjectName = "CursoTeste2", CollegeId = 3 });
                _context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "T3", SubjectName = "CursoTeste3", CollegeId = 3 });
                _context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "T4", SubjectName = "CursoTeste4", CollegeId = 4 });
                _context.SaveChanges();
            }

            if (!_context.ApplicationStats.Any())
            {
                _context.ApplicationStats.Add(new ApplicationStat { Name = "Avaliação Pendente" });
                _context.ApplicationStats.Add(new ApplicationStat { Name = "Em Avaliação" });
                _context.ApplicationStats.Add(new ApplicationStat { Name = "Seriação Pendente" });
                _context.ApplicationStats.Add(new ApplicationStat { Name = "Aprovado" });
                _context.ApplicationStats.Add(new ApplicationStat { Name = "Reprovado" });
                _context.ApplicationStats.Add(new ApplicationStat { Name = "Finalizado" });
                _context.SaveChanges();
            }

            if (!_context.BilateralProtocols.Any())
            {
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "Lithuania", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Spain", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "Testing1", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Testing2", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "Lithuania", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Testing4", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "Testing5", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Testing6", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 1, Destination = "Testing7", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 2, Destination = "Testing8", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 3, Destination = "Testing9", OpenSlots = 1 });
                _context.BilateralProtocols.Add(new BilateralProtocol { SubjectId = 4, Destination = "Testing10", OpenSlots = 1 });
                _context.SaveChanges();
            }

            if (!_context.Students.Any())
            {
                _context.Students.Add(new Student
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
                });
                _context.Students.Add(new Student
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
                });
                _context.SaveChanges();
            }

            if (!_context.Employees.Any())
            {
                _context.Employees.Add(new Employee
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
                });

                _context.Employees.Add(new Employee
                {
                    UserName = "testemployee1@cimob.pt",
                    UserFullname = "Empregado Teste1",
                    Email = "testemployee1@cimob.pt",
                    UserCc = "12345689",
                    PhoneNumber = "936936936",
                    UserAddress = "RuaTeste",
                    PostalCode = "2900-000",
                    BirthDate = new DateTime(1996, 1, 1),
                    EmployeeNumber = "150221055"
                });
                _context.SaveChanges();
            }

            if (!_context.Applications.Any())
            {

                _context.Applications.Add(new Application
                {
                    StudentId = _context.Students.Where(s => s.UserFullname.Equals("Teste User 1")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 1,
                    BilateralProtocol2Id = 5,
                    BilateralProtocol3Id = 9,
                    EmployeeId = _context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2018, 01, 03),
                    ArithmeticMean = 20.0,
                    ECTS = 120,
                    MotivationLetter = 20.0,
                    Interview = 20.0,
                    FinalGrade = 20.0
                });

                _context.Applications.Add(new Application
                {
                    StudentId = _context.Students.Where(s => s.UserFullname.Equals("Teste User 2")).FirstOrDefault().Id,
                    ApplicationStatId = 3,
                    BilateralProtocol1Id = 1,
                    BilateralProtocol2Id = 5,
                    BilateralProtocol3Id = 9,
                    EmployeeId = _context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                    CreationDate = new DateTime(2018, 01, 03),
                    ArithmeticMean = 10.0,
                    ECTS = 120,
                    MotivationLetter = 10.0,
                    Interview = 10.0,
                    FinalGrade = 10.0
                });

                _context.SaveChanges();
            }

            if (!_context.Editals.Any())
            {
                _context.Editals.Add(new Edital { Title = "Edital teste", TextContent = "Edital publicado", OpenDate = new DateTime(2017, 11, 10), CloseDate = new DateTime(2018, 04, 03) });
                _context.SaveChanges();
            }

            if (!_context.ApplicationStatHistory.Any())
            {
                _context.ApplicationStatHistory.Add(new ApplicationStatHistory { ApplicationId = 1, ApplicationStat = "Avaliação Pendente", DateOfUpdate = DateTime.Now });
                _context.ApplicationStatHistory.Add(new ApplicationStatHistory { ApplicationId = 1, ApplicationStat = "Em Avaliação", DateOfUpdate = DateTime.Now });
                _context.SaveChanges();
            }

        }



        [Fact]
        public async Task TestingEvaluateApplicationSuccessfull()
        {
            InitializeDatabaseWithDataTest();
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            Application appTest = _context.Applications.SingleOrDefault(a => a.ApplicationId == 1);
            Application appModelTest = new Application
            {
                ApplicationId = appTest.ApplicationId,
                StudentId = _context.Students.Where(s => s.UserFullname.Equals("Teste User 1")).FirstOrDefault().Id,
                ApplicationStatId = 3,
                BilateralProtocol1Id = 2,
                BilateralProtocol2Id = 6,
                BilateralProtocol3Id = 10,
                EmployeeId = _context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id,
                CreationDate = new DateTime(2017, 11, 13),
                ArithmeticMean = 15.0,
                ECTS = 66,
                MotivationLetter = 15.0,
                Interview = 15.0,
                FinalGrade = 15.0
            };
            var result = await controller.Edit(appTest.ApplicationId, appModelTest);
            _context.Entry(appTest).State = EntityState.Detached;
            appTest = _context.Applications.SingleOrDefault(a => a.ApplicationId == 1);
            Assert.Equal(15, appTest.FinalGrade);
        }

        [Fact]
        public async Task TestingFilterAssignedEmployee()
        {
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            String employeeId = _context.Employees.SingleOrDefault(a => a.UserFullname.Equals("Empregado Teste")).Id;
            var result = await controller.Filter("CurrentlySupervising", employeeId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Application>>(
                viewResult.ViewData.Model);
            
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task TestingFilterNotAssigned()
        {
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            String employeeId = _context.Employees.SingleOrDefault(a => a.UserFullname.Equals("Empregado Teste")).Id;
            var result = await controller.Filter("NotSupervising", employeeId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Application>>(
                viewResult.ViewData.Model);

            Assert.Empty(model);
        }

        [Fact]
        public async Task TestingNoFilter()
        {
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            String employeeId = _context.Employees.SingleOrDefault(a => a.UserFullname.Equals("Empregado Teste")).Id;
            var result = await controller.Filter("", employeeId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Application>>(
                viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task TestingSeriationSuccessfull()
        {
            InitializeDatabaseWithDataTest();
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            await controller.Seriation();
            // Assert
            Assert.Equal(0, _context.Applications.Count(a => a.ApplicationStatId == 3));
        }

        [Fact]
        public async Task TestingSeriationNotSuccessfull()
        {
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            _context.Applications.First().ApplicationStatId = 1;
            _context.SaveChanges();
            var result = await controller.Seriation();

            // Assert

            Assert.Equal(1, _context.Applications.Count(a => a.ApplicationStatId == 3));
        }

        [Fact]
        public async Task TestingAssignEmployeeSuccessfull()
        {
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            _context.Applications.First().EmployeeId = null;
            _context.SaveChanges();
            String employeeId = _context.Employees.SingleOrDefault(a => a.UserFullname.Equals("Empregado Teste")).Id;
            await controller.AssignEmployee(employeeId, 1);
            // Assert

            Assert.Equal(employeeId, _context.Applications.First().EmployeeId);
        }

        [Fact]
        public async Task TestingAssignEmployeeFailed()
        {
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            String employeeId1 = _context.Employees.SingleOrDefault(a => a.UserFullname.Equals("Empregado Teste1")).Id;
            String employeeId2 = _context.Employees.SingleOrDefault(a => a.UserFullname.Equals("Empregado Teste")).Id;
            await controller.AssignEmployee(employeeId1, 1);
            // Assert

            Assert.Equal(employeeId2, _context.Applications.First().EmployeeId);
        }

        [Fact]
        public async Task TestingDisplaySeriationAfterSeriation()
        {
            InitializeDatabaseWithDataTest();
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            await controller.Seriation();
            var result = await controller.DisplaySeriation();
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Application>>(
                viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task TestingDisplaySeriationBeforeSeriation()
        {
            InitializeDatabaseWithDataTest();
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            var result = await controller.DisplaySeriation();
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Application>>(
                viewResult.ViewData.Model);

            Assert.Empty(model);
        }

        [Fact]
        public async Task TestingApplicationHistory()
        {
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            String studentId = _context.Students.Where(s => s.UserFullname.Equals("Teste User 1")).FirstOrDefault().Id;
            var result = await controller.ApplicationHistory(studentId);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ApplicationStatHistory>>(
                viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task TestingApplicationClosingSuccessfull()
        {
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            var application = await _context.Applications.SingleOrDefaultAsync(a => a.ApplicationId == 1);
            application.ApplicationStatId = 4;
            _context.SaveChanges();
            String currentEmployee = _context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id;
            await controller.FinishApplication(application.ApplicationId, currentEmployee);

            _context.Entry(application).State = EntityState.Detached;

            application = await _context.Applications.SingleOrDefaultAsync(a => a.ApplicationId == 1);
            // Assert

            Assert.Equal(6, application.ApplicationStatId);
        }

        [Fact]
        public async Task TestingApplicationClosingFailed()
        {
            ApplicationsController controller = new ApplicationsController(_context);
            // Act
            var application = await _context.Applications.SingleOrDefaultAsync(a => a.ApplicationId == 1);
            application.ApplicationStatId = 2;
            _context.SaveChanges();
            String currentEmployee = _context.Employees.Where(s => s.UserFullname.Equals("Empregado Teste")).FirstOrDefault().Id;
            await controller.FinishApplication(application.ApplicationId, currentEmployee);

            _context.Entry(application).State = EntityState.Detached;

            application = await _context.Applications.SingleOrDefaultAsync(a => a.ApplicationId == 1);
            // Assert
            Assert.Equal(2, application.ApplicationStatId);
        }

    }
}