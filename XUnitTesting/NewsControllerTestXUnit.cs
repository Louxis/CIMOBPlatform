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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTesting
{
    public class NewsControllerTestXunit
    {
        private IConfigurationRoot _configuration;

        // represents database's configuration
        private DbContextOptions<ApplicationDbContext> _options;
        private ApplicationDbContext _context;

        public NewsControllerTestXunit()
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
                _context.SaveChanges();
            }

            if (!_context.CollegeSubjects.Any())
            {
                _context.CollegeSubjects.Add(new CollegeSubject { SubjectAlias = "EI", SubjectName = "Engenharia Informática", CollegeId = 1 });
                _context.SaveChanges();
            }

                var user1 = new Student
                {
                    UserName = "test@test",
                    UserFullname = "Teste User 1",
                    Email = "test@test",
                    UserCc = "12345679",
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123123123",
                    ALOGrade = 0,
                    CollegeSubjectId = 1
                };
            _context.Students.Add(user1);
            _context.SaveChanges();

                var user2 = new Employee
                {
                    UserName = "test1@test",
                    UserFullname = "Teste User 2",
                    Email = "test1@test",
                    UserCc = "12345689",
                    PhoneNumber = "936936938",
                    UserAddress = "Avenida para teste1",
                    PostalCode = "2912-124",
                    BirthDate = new DateTime(1995, 2, 1),
                    EmployeeNumber = "123123321"
                };
            _context.Employees.Add(user2);
            _context.SaveChanges();

            var news1 = new News
                {
                    EmployeeId = _context.Employees.Where(s => s.UserFullname.Equals("Teste User 2")).FirstOrDefault().Id,
                    IsPublished = false,
                    TextContent = "teste1",
                    Title = "teste1"
                };

            var news2 = new News
                {
                    EmployeeId = _context.Employees.Where(s => s.UserFullname.Equals("Teste User 2")).FirstOrDefault().Id,
                    IsPublished = true,
                    TextContent = "teste2",
                    Title = "teste2"
                };

            var news3 = new News
                {
                    EmployeeId = _context.Employees.Where(s => s.UserFullname.Equals("Teste User 2")).FirstOrDefault().Id,
                    IsPublished = true,
                    TextContent = "teste3",
                    Title = "teste3"
                };

            var news4 = new News
                {
                    EmployeeId = _context.Employees.Where(s => s.UserFullname.Equals("Teste User 2")).FirstOrDefault().Id,
                    IsPublished = false,
                    TextContent = "teste4",
                    Title = "teste4"
                };

            var news5 = new News
                {
                    EmployeeId = _context.Employees.Where(s => s.UserFullname.Equals("Teste User 2")).FirstOrDefault().Id,
                    IsPublished = true,
                    TextContent = "teste5",
                    Title = "teste5"
                };
            _context.SaveChanges();

            _context.News.Add(news1);
            _context.News.Add(news2);
            _context.News.Add(news3);
            _context.News.Add(news4);
            _context.News.Add(news5);
            _context.SaveChanges();
            }

        [Fact]
        public async Task TestingRecentNews()
        {
            InitializeDatabaseWithDataTest();
            NewsController controller = new NewsController(_context);
            // Act
            var result = await controller.RecentNews();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<News>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async void TestingPublishNews()
        {
            InitializeDatabaseWithDataTest();
            NewsController controller = new NewsController(_context);
            News toPublish = _context.News.FirstOrDefault();
            Employee employee = await _context.Employees.FirstOrDefaultAsync();
            await controller.Publish(toPublish.Id);
            var result = _context.News.SingleOrDefault(n => n.Id == 1);
            Assert.True(result.IsPublished);
        }
    }
}
