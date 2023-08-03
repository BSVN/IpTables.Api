using BSN.IpTables.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTables.Api.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [SetUp]
        public void Initialize()
        {
            controller = new HomeController();
        }

        [Test]
        public void IndexNormalCalling_ShloudReturnsOk()
        {
            IActionResult result = controller.Index(); 
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(OkResult)));
        }

        [TearDown] public void TearDown() { }

        private HomeController controller;
    }
}
