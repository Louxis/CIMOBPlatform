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

namespace XUnitTesting
{

    public class StudentControllerTests
    {
        private List<Student> list = new List<Student>();

        internal void Populate()
        {
            list.Clear();
            Student user1 = new Student
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

            Student user2 = new Student
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

            Student user3 = new Student
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

            Student user4 = new Student
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
            
            list.Add(user1);
            list.Add(user2);
            list.Add(user3);
            list.Add(user4);
        }
        [Fact]
        public void TestingSearchBySpecificName()
        {
            Populate();
            var filteredStudents = list.Where(s => s.UserFullname.Contains("Random"));
            String test = filteredStudents.First().UserName;
            Assert.Equal("Random user", filteredStudents.First().UserFullname);
        }
        [Fact]
        public void TestingSearchByGenericName()
        {
            Populate();
            var filteredStudents = list.Where(s => s.UserFullname.Contains("Test"));
            Assert.Equal(2, filteredStudents.Count());
        }
        [Fact]
        public void TestingSearchWithNonExistentName()
        {
            Populate();
            var filteredStudents = list.Where(s => s.UserFullname.Contains("WrongName"));
            Assert.Empty(filteredStudents);
        }

        [Fact]
        public void TestingSearchBySpecificNumber()
        {
            Populate();
            var filteredStudents = list.Where(s => s.StudentNumber.Contains("123123321"));
            Assert.Equal("123123321", filteredStudents.First().StudentNumber);
        }
        [Fact]
        public void TestingSearchByGenericNumber()
        {
            Populate();
            var filteredStudents = list.Where(s => s.StudentNumber.Contains("123123"));
            Assert.Equal(2, filteredStudents.Count());
        }
        [Fact]
        public void TestingSearchWithNonExistentNumber()
        {
            Populate();
            var filteredStudents = list.Where(s => s.StudentNumber.Contains("000000000"));
            Assert.Empty(filteredStudents);
        }

        [Fact]
        public void TestingSearchBySpecificMail()
        {
            Populate();
            var filteredStudents = list.Where(s => s.Email.Contains("random@random"));
            Assert.Equal("random@random", filteredStudents.First().Email);
        }
        [Fact]
        public void TestingSearchByGenericMail()
        {
            Populate();
            var filteredStudents = list.Where(s => s.Email.Contains("test"));
            Assert.Equal(2, filteredStudents.Count());
        }
        [Fact]
        public void TestingSearchWithNonExistentMail()
        {
            Populate();
            var filteredStudents = list.Where(s => s.Email.Contains("noMail"));
            Assert.Empty(filteredStudents);
        }

        [Fact]
        public void TestingSearchByCollege()
        {
            Populate();
            var filteredStudents = list.Where(s => s.CollegeSubjectId == 1);
            Assert.Equal(4, filteredStudents.Count());
        }
        [Fact]
        public void TestingSearchWithNonExistentCollege()
        {
            Populate();
            var filteredStudents = list.Where(s => s.CollegeSubjectId == 3);
            Assert.Empty(filteredStudents);
        }
    }
}
