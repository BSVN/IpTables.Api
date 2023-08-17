using AutoMapper;
using BSN.Commons.Responses;
using BSN.IpTables.Api.Controllers.V1;
using BSN.IpTables.Domain;
using BSN.IpTables.Presentation.Dto.V1;
using BSN.IpTables.Presentation.Dto.V1.ViewModels;
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
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AppServiceViewMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            controller = new HomeController(mockLoggerFactory.Object, mapper, mockIpTablesSystem.Object);
        }

        [Test]
        public void ListNormalCalling_ShloudReturnsOk()
        {
            ActionResult<Response<IpTablesChainSetViewModel>> result = controller.List().Result; 
            Assert.That(result, Is.Not.Null);
        }

        [TearDown] public void TearDown() { }

        private HomeController controller;
    }
}
