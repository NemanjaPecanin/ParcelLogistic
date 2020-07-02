using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities.Exceptions;
using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;
using ParcelLogistics.SKS.Package.Services.Attributes;
using ParcelLogistics.SKS.Package.Services.Controllers;
using ParcelLogistics.SKS.Package.Services.DTOs;
using ParcelLogistics.SKS.Package.Services.Filters;
using ParcelLogistics.SKS.Package.Services.Mapper;

namespace ParcelLogistics.SKS.Package.Tests
{
    public class WarehouseManagementApiControllerTests
    {

        private Mock<IExportImportLogic> _mock;
        private WarehouseManagementApiController _controller;
        private readonly IMapper _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();

        [SetUp]
        public void Setup()
        {
            _mock = new Mock<IExportImportLogic>();
            _controller = new WarehouseManagementApiController(_mapper, _mock.Object);
        }

        [Test]
        public void ExportWarehouses_Succeeded()
        {
            var result = _controller.ExportWarehouses();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);
        }

        [Test]
        public void ExportWarehouses_Failed_BlNotFoundException()
        {
            var result = _controller.ExportWarehouses();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void ImportWarehouses_Succeeded()
        {
            var result = _controller.ImportWarehouses(new Warehouse());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ObjectResult>(result);
        }

        [Test]
        public void ImportWarehouses_Failed()
        {
            var result = _controller.ImportWarehouses(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }


    }
}