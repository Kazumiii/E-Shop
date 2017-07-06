using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore2.WebUI.Controllers;
using SportsStore2.WebUI.Infrasctucture.Abstract;
using SportsStore2.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportStore.UnitTest
{
    [TestClass]
    class AdminSecurityTest
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "sekret")).Returns(true);

            LoginViewModel model = new LoginViewModel()
            {
                UserName = "admin",
                Password = "sekret",
            };

            AccountController target = new AccountController(mock.Object);
            ActionResult result = target.Login(model, "/MyUrl");

            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyUrl", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("NieprawidłowaNazwa", "NieprawidłoweHasło")).Returns(false);

            LoginViewModel model = new LoginViewModel()
            {
                UserName = "NieprawidłowaNazwa",
                Password = "NieprawidłoweHasło",
            };

            AccountController controller = new AccountController(mock.Object);
            ActionResult result = controller.Login(model, "/MyUrl");

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

        }

    }
}
