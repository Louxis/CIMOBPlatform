using CIMOBProject.Controllers;
using CIMOBProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CIMOBProject.Services;
using Microsoft.Extensions.Logging;
using CIMOBProject.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace CIMOBProject.xUnit
{
    public class StudentControllerTest {

        private IConfigurationRoot _configuration;

        // represents database's configuration
        private DbContextOptions<ApplicationDbContext> _options;
        private ApplicationDbContext _context;

        public StudentControllerTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .Options;
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
                _context.SaveChanges();
            }

            if (!_context.Students.Any())
            {
                var user1 = new Student
                {
                    UserName = "test@test",
                    UserFullname = "Teste User 1",
                    Email = "test@test",
                    UserCc = "123456789",
                    PhoneNumber = "936936936",
                    UserAddress = "Avenida para teste",
                    PostalCode = "2912-123",
                    BirthDate = new DateTime(1995, 1, 1),
                    StudentNumber = "123123123",
                    ALOGrade = 0,
                    CollegeSubjectId = 1
                };

                var user2 = new Student
                {
                    UserName = "test1@test",
                    UserFullname = "Teste User 2",
                    Email = "test1@test",
                    UserCc = "123456789",
                    PhoneNumber = "936936938",
                    UserAddress = "Avenida para teste1",
                    PostalCode = "2912-124",
                    BirthDate = new DateTime(1995, 2, 1),
                    StudentNumber = "123123321",
                    ALOGrade = 0,
                    CollegeSubjectId = 1
                };

                var user3 = new Student
                {
                    UserName = "stuff@stuff",
                    UserFullname = "Stuff user",
                    Email = "stuff@stuff",
                    UserCc = "123456789",
                    PhoneNumber = "936936899",
                    UserAddress = "Avenida para stuff",
                    PostalCode = "2912-193",
                    BirthDate = new DateTime(1995, 3, 1),
                    StudentNumber = "321321321",
                    ALOGrade = 0,
                    CollegeSubjectId = 1
                };

                var user4 = new Student
                {
                    UserName = "random@random",
                    UserFullname = "Random user",
                    Email = "random@random",
                    UserCc = "123456789",
                    PhoneNumber = "936936999",
                    UserAddress = "Avenida random",
                    PostalCode = "2912-200",
                    BirthDate = new DateTime(1995, 3, 2),
                    StudentNumber = "987987987",
                    ALOGrade = 0,
                    
                    CollegeSubjectId = 1
                    
                };

                _context.ApplicationUsers.Add(user1);
                _context.ApplicationUsers.Add(user2);
                _context.ApplicationUsers.Add(user3);
                _context.ApplicationUsers.Add(user4);
                _context.SaveChanges();
            }
        }


        [Fact]
        public async Task TestingSearchBySpecificName()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("studentName", "Random");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Equal("Random user", model.First().UserFullname);
        }
        [Fact]
        public async Task TestingSearchByGenericName()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("studentName", "Test");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        [Fact]
        public async Task TestingSearchWithNonExistentName()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("studentName", "NoName");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Empty(model);


        }

        [Fact]
        public async Task TestingSearchBySpecificNumber()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("studentNumber", "123123321");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Equal("123123321", model.First().StudentNumber);
        }
        [Fact]
        public async Task TestingSearchByGenericNumber()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("studentNumber", "123123");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        [Fact]
        public async Task TestingSearchWithNonExistentNumber()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("studentNumber", "000000000");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Empty(model);
        }

        [Fact]
        public async Task TestingSearchBySpecificMail()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("mail", "random@random");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Equal("random@random", model.First().Email);
        }
        [Fact]
        public async Task TestingSearchByGenericMail()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("mail", "test");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        [Fact]
        public async Task TestingSearchWithNonExistentMail()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("mail", "noMail");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Empty(model);
        }

        [Fact]
        public async Task TestingSearchBySpecificCollege()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("studentCollege", "Escola Superior de Tecnologia de Setúbal");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Equal(4, model.Count());
        }
        [Fact]
        public async Task TestingSearchByGenericCollege()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("studentCollege", "Escola");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Equal(4, model.Count());
        }
        [Fact]
        public async Task TestingSearchWithNonExistentCollege()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            // Act
            var result = await controller.Search("studentCollege", "NoSchool");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Empty(model);
        }

        [Fact]
        public async Task TestingCheckStudentDetails()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            string studentIndex = _context.Students.Where(s => s.UserFullname.Equals("Random user")).FirstOrDefault().Id;
            // Act
            var result = await controller.Details(studentIndex);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.ViewData.Model);
            Assert.Equal("Random user",model.First().UserFullname);
        }

        [Fact]
        public async Task TestingUpdateStudentDetails()
        {
            InitializeDatabaseWithDataTest();
            StudentsController controller = new StudentsController(_context);
            string studentIndex = _context.Students.Where(s => s.UserFullname.Equals("Random user")).FirstOrDefault().Id;

            var user = new Student
            {
                UserName = "random@random",
                UserFullname = "New Random user",
                Email = "random@random",
                UserCc = "123456789",
                PhoneNumber = "936936999",
                UserAddress = "Avenida random",
                PostalCode = "2912-200",
                BirthDate = new DateTime(1995, 3, 2),
                StudentNumber = "987987987",
                ALOGrade = 20,
                CollegeSubjectId = 1,
                Id = studentIndex
            };
            // Act
            var result = await controller.Edit(studentIndex, user);

            // Assert
            /*var viewResult = Assert.IsType<RedirectToActionResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(
                viewResult.RouteValues);*/
            Assert.Equal(20, _context.Students.Where(m => m.Id == studentIndex).First().ALOGrade);
        }

    }
}
