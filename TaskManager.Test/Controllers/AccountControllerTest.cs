using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using TaskManager.MVC.Web.Controllers;
using TaskManager.MVC.Web.Models;
using TaskManager.MVC.Web.Repositories;
using TaskManager.MVC.Web.Session;
using Xunit;

namespace TaskManager.Test.Controllers
{
    public class AccountControllerTest
    {
        private IUserRepository _userRepository;

        private IAdminSession _adminSession;

        public AccountControllerTest()
        {
            Mock<IUserRepository> mockRep = new Mock<IUserRepository>();
            mockRep.Setup(m => m.FindAll()).Returns(new List<UserModel> {
            new UserModel { Id = 1, UserName = "chenmeiyi", Password = "cmy123456" }
            });

            mockRep.Setup(m => m.FindByUserName(It.Is<string>(name => name == "chenmeiyi"))).Returns(new UserModel{
                Id = 1,
                UserName = "chenmeiyi",
                Password = "cmy123456"
            });

            mockRep.Setup(m => m.IsValid(It.Is<string>(userName => userName == "chenmeiyi"), It.Is<string>(password => password == "cmy123456"))).Returns(true);

            var mockSession = new Mock<IAdminSession>();

            _userRepository = mockRep.Object;

            _adminSession = mockSession.Object;
        }

        [Fact]
        public void LoginShouldReturnView()
        {
            // Arrange
            var controller = new AccountController(_userRepository, _adminSession);

            // Act
            var actionResult = controller.Login();

            // Assert
            Assert.NotNull(actionResult);
        }

        [Fact]
        public void LoginSuccesssShouldRedirectToTaskIndex()
        {
            var controller = new AccountController(_userRepository, _adminSession);
            var actionResult = controller.Login(new UserModel { Id = 1, UserName = "chenmeiyi", Password = "cmy123456" }) as RedirectToActionResult;
            
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Equal("Task", actionResult.ControllerName);
        }

        [Fact]
        public void LoginInvaildShouldReturnView()
        {
            var controller = new AccountController(_userRepository, _adminSession);
            var model = new UserModel { Id = 1, UserName = "chenmeiyi", Password = "cmy123" };
            var actionResult = controller.Login(model) as ViewResult;
            Assert.Equal(model, actionResult.Model);
        }
    }
}
