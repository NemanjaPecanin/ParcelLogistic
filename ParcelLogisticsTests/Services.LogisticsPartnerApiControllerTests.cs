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
    class LogisticsPartnerApiControllerTests
    {
        private Mock<ITrackingLogic> _mock;
        private LogisticsPartnerApiController _controller;
        private readonly IMapper _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();

        [SetUp]
        public void Setup()
        {
           _mock = new Mock<ITrackingLogic>();
           _controller = new LogisticsPartnerApiController(_mapper, _mock.Object);
        }

        [Test]
        public void TransitionParcel_Succeeded()
        {
            var id = "ABCD56789";
            var result = _controller.TransitionParcel(new Parcel(), id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public void TransitionParcel_Succeeded2()
        {
            var id = "ABCD56789";
            var result = _controller.TransitionParcel(new Parcel(), id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void TransitionParcel_Failed_Invalid_TrackingId()
        {
            var result = _controller.TransitionParcel(new Parcel(), null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
       }

        [Test]
        public void TransitionParcel_Failed_TrackingIdEmpty()
        {
            var result = _controller.TransitionParcel(new Parcel(), string.Empty);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
