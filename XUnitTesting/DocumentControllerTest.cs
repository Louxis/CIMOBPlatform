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
    public class DocumentControllerTest
    {
        private IConfigurationRoot _configuration;

        // represents database's configuration
        private DbContextOptions<ApplicationDbContext> _options;
        private ApplicationDbContext _context;

        public DocumentControllerTest()
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
                    UserCc = 123456789,
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
                    UserCc = 123456800,
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
                    UserCc = 123456100,
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
                    UserCc = 123456200,
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
            if (!_context.Documents.Any())
            {

                var doc1 = new Document
                {
                    Description = "Test document1",
                    FileUrl = "URL.COM",
                    StudentId = _context.Students.Where(s => s.UserFullname.Equals("Random user")).FirstOrDefault().Id
                };

                var doc2 = new Document
                {
                    Description = "Test document2",
                    FileUrl = "URL.COM",
                    StudentId = _context.Students.Where(s => s.UserFullname.Equals("Random user")).FirstOrDefault().Id
                };

                var doc3 = new Document
                {
                    Description = "Test document3",
                    FileUrl = "URL.COM",
                    StudentId = _context.Students.Where(m => m.UserFullname == "Stuff user").First().Id
                };
                _context.Documents.Add(doc1);
                _context.Documents.Add(doc2);
                _context.Documents.Add(doc3);
                _context.SaveChanges();
            }
        }


        [Fact]
        public async Task TestingGetDocumentsFromStudent()
        {
            InitializeDatabaseWithDataTest();
            DocumentsController controller = new DocumentsController(_context);

            

            string studentIndex = _context.Students.Where(s => s.UserFullname.Equals("Random user")).FirstOrDefault().Id;
            // Act
            var result = await controller.Index(studentIndex);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Document>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        [Fact]
        public async Task TestingGetDocumentsFromStudentWithNoDocuments()
        {
            InitializeDatabaseWithDataTest();
            DocumentsController controller = new DocumentsController(_context);

            string studentIndex = _context.Students.Where(m => m.UserFullname == "Teste User 1").First().Id;
            // Act
            var result = await controller.Index(studentIndex);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Document>>(
                viewResult.ViewData.Model);
            Assert.Empty(model);
        }
    }
}
