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
        private UserStore userStore;
        private UserManager<ApplicationDbContext> manager;

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


        public async Task SignInIntegrationTestUser(HttpContext context)
        {
            var integrationTestsUserHeader = context.Request.Headers["IntegrationTestLogin"];
            if (integrationTestsUserHeader.Count > 0)
            {
                var userName = integrationTestsUserHeader[0];
                var userManager = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await userManager.FindByEmailAsync(userName);
                if (user == null)
                {
                    return;
                }
                var signInManager = context.RequestServices.GetRequiredService<SignInManager<ApplicationUser>>();
                var userIdentity = await signInManager.CreateUserPrincipalAsync(user);
                context.User = userIdentity;
            }
        }

        public void InitializeDatabaseWithDataTest()
        {
            

            _context = new ApplicationDbContext(_options);

            var userStore = new UserStore<ApplicationUser>(_context);
            //userStore = new UserStore(_context);

            var userManager = new ApplcationUserManager(userStore);
            //var manager = _context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            manager = new UserManager<ApplicationUser>(userStore, null, null, null,null, null,null,null, null);

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

                _context.SaveChanges();
            }
        }

        [Fact]
        public async Task TestSearchByNumber()
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

    }
}
