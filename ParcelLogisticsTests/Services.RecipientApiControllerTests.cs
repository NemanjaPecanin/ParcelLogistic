using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;
using ParcelLogistics.SKS.Package.Services.Controllers;
using ParcelLogistics.SKS.Package.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.Tests
{
    class RecipientApiControllerTests
    {
        private Mock<ITrackingLogic> _mock;
        private ReceipientApiController _controller;
        private readonly IMapper _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();

        [SetUp]
        public void Setup()
        {
            _mock = new Mock<ITrackingLogic>();
            _controller = new ReceipientApiController(_mock.Object, _mapper);
        }

        [Test]
        public void SubmitParcel_Failed_ParcelNull()
        {
            var result = _controller.TrackParcel(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public void SubmitParcel_Succeeded()
        {
            var result = _controller.TrackParcel("123456789");

            Assert.IsNotNull(result);
            //Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
