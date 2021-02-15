using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.WebUI.Infrastructure.Abstract;
using BookStore.WebUI.Models;
using BookStore.WebUI.Controllers;
using System.Web.Mvc;

namespace BookStore.UnitTest
{
    [TestClass]
    public class Admin_Sec
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credintials()
        {
            //Arrange
            Mock<IAuthenticationProvider> mock = new Mock<IAuthenticationProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
            LoginViewModel model = new LoginViewModel { Username="admin",Password="secret"};
            AccountController target = new AccountController(mock.Object);

            //Act
            ActionResult result = target.Login(model,"URL");
            //Assert
            Assert.IsInstanceOfType(result,typeof(RedirectResult));
            Assert.AreEqual("URL",((RedirectResult) result).Url);

        }
        [TestMethod]
        public void Can_Login_With_Ivalid_Credintials()
        {
            //Arrange
            Mock<IAuthenticationProvider> mock = new Mock<IAuthenticationProvider>();
            mock.Setup(m => m.Authenticate("userx", "passx")).Returns(false);
            LoginViewModel model = new LoginViewModel { Username = "userx", Password = "passx" };
            AccountController target = new AccountController(mock.Object);

            //Act
            ActionResult result = target.Login(model, "URL");
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

        }
    }
}
