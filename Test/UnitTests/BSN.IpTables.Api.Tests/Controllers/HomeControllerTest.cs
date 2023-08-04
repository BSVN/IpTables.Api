using BSN.IpTables.Api.Controllers;
using BSN.IpTables.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
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
            var mockLogger = new Mock<ILogger<HomeController>>();
            mockLogger.Setup(
                m => m.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()));

            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(() => mockLogger.Object);

            var mockIpTablesSystem = new Mock<IIpTablesSystem>();
            controller = new HomeController(mockLoggerFactory.Object, mockIpTablesSystem.Object);
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
