using CIMOBProject.Controllers;
using CIMOBProject.Data;
using CIMOBProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTesting
{
    public class QuizzControllerTestXUnit
    {
        private IConfigurationRoot _configuration;

        // represents database's configuration
        private DbContextOptions<ApplicationDbContext> _options;
        private ApplicationDbContext _context;
        private const int GENERATE_DEFAULT_QUIZZ = 1;

        public QuizzControllerTestXUnit() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .Options;
            InitializeDatabaseWithDataTest();
        }

        internal void InitializeDatabaseWithDataTest() {
            _context = new ApplicationDbContext(_options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.Quizzs.Add(new Quizz() {
                Year = 2012,
                Semester = 1,
                QuizzUrl = "www.quizz.test",
                IsPublished = false
            });
            _context.Employees.Add(new Employee {
                UserName = "testemployee1@cimob.pt",
                UserFullname = "Empregado Teste1",
                Email = "testemployee1@cimob.pt",
                UserCc = "12345689",
                PhoneNumber = "936936936",
                UserAddress = "RuaTeste",
                PostalCode = "2900-000",
                BirthDate = new DateTime(1996, 1, 1),
                EmployeeNumber = 150221055
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task TestingGetQuizzs() {
            QuizzsController controller = new QuizzsController(_context);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Quizz>>(
                viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public void TestingPublishQuizz() {
            QuizzsController controller = new QuizzsController(_context);
            Quizz toPublish = _context.Quizzs.FirstOrDefault();
            Employee employee = _context.Employees.FirstOrDefault();
            var result = controller.Publish(toPublish.Id, employee.Id);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);        
        }

        [Fact]
        public async Task TestingCreateQuizz() {
            //makes sure db starts with default number of quizz
            InitializeDatabaseWithDataTest();
            QuizzsController controller = new QuizzsController(_context);
            Quizz testQuizz = new Quizz() {
                Year = 2012,
                Semester = 2,
                QuizzUrl = "www.quizz2.test",
                IsPublished = true
            };
            await controller.Create(testQuizz);
            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Quizz>>(
                viewResult.ViewData.Model);
            Assert.Equal(GENERATE_DEFAULT_QUIZZ + 1, model.Count());
        }
    }
}
