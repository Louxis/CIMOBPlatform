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

namespace CIMOBProject.xUnit
{
    public class AccountControllerTest
    {
        //[Fact]
        //public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        //{
        //    // Arrange
            
        //    var mockUserManager = new Mock<UserManager<ApplicationUser>>();
        //    var mockSignIn = new Mock<SignInManager<ApplicationUser>>();
        //    var mockEmail = new Mock<IEmailSender>();
        //    var mockLogging = new Mock<ILogger<AccountController>>();
        //    var mockDb= new Mock<ApplicationDbContext>();

        //    mockDb.Setup(db => db.)

        //    mockUserManager.Setup(repo => repo.Users).Returns();

        //    var controller = new AccountController(mockUserManager.Object, mockSignIn.Object, mockEmail.Object, mockLogging.Object, mockDb.Object);

        //    // Act
        //    var result = await controller.Index();

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
        //        viewResult.ViewData.Model);
        //    Assert.Equal(2, model.Count());
        //}
    }
}
