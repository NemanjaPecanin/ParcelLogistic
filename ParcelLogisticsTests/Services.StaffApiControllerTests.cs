using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;
using ParcelLogistics.SKS.Package.Services.Attributes;
using ParcelLogistics.SKS.Package.Services.Controllers;
using ParcelLogistics.SKS.Package.Services.DTOs;
using ParcelLogistics.SKS.Package.Services.Filters;
using ParcelLogistics.SKS.Package.Services.Mapper;

namespace ParcelLogistics.SKS.Package.Tests
{
    class StaffApiControllerTests
    {
        private Mock<ITrackingLogic> _mock;
        private StaffApiController _controller;
        private readonly IMapper _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();

        [SetUp]
        public void Setup()
        {
            _mock = new Mock<ITrackingLogic>();
           _controller = new StaffApiController(_mock.Object, _mapper);
        }

        [Test]
        public void ReportParcelDelivery_Succeeded()
        {
            var result = _controller.ReportParcelDelivery("ABCD56789");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void ReportParcelHop_Succeeded()
        {
            var result = _controller.ReportParcelHop("ABCD56789", "WTTA040");

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void ReportParcelDelivery_Failed()
        {
            var result = _controller.ReportParcelDelivery(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void ReportParcelDelivery_Failed_TrackingIdEmpty()
        {
            var result = _controller.ReportParcelDelivery(string.Empty);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}
