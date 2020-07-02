using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.BusinessLogic;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities.Exceptions;
using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;
using ParcelLogistics.SKS.Package.BusinessLogic.Tests;
using ParcelLogistics.SKS.Package.Services.Controllers;
using ParcelLogistics.SKS.Package.Services.DTOs;
using ParcelLogistics.SKS.Package.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.Tests
{
    class SenderApiControllerTests
    {
        private Mock<ITrackingLogic> _mock;
        private SenderApiController _controller;
        private readonly IMapper _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();

        [SetUp]
        public void Setup()
        {
            _mock = new Mock<ITrackingLogic>();
            _controller = new SenderApiController(_mapper, _mock.Object);
        }

        [Test]
        public void SubmitParcel_Failed_ParcelNull()
        {
            var result = _controller.SubmitParcel(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void SubmitParcel_Succeeded()
        {
            //var result = _controller.SubmitParcel(_mapper.Map<Parcel>(MockBuilder.NotNgBuilderParcel()));

            Assert.IsNotNull("");
            //Assert.IsInstanceOf<OkObjectResult>(result);
        }

    }
}