using Microsoft.VisualStudio.TestTools.UnitTesting;
using RentCarApplication.Controllers;
using RentCarApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ApplicationServices;

namespace RentCarApplication.Controllers.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            //string userr = "bimix";
            //string pass = "bimix";

            var controller = new UserController();

            var user = controller.Login();

            Assert.AreEqual(user, "bimix");

        }

        
    }
}