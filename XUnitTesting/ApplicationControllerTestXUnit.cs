using CIMOBProject.Data;
using CIMOBProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XUnitTesting
{
    class ApplicationControllerTestXUnit
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
                    UserCc = "12345679",
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
                    UserCc = "12345689",
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
                    UserCc = "12345789",
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
                    UserCc = "12346789",
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


    }
}
