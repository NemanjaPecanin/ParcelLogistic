using AutoMapper;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities.Exceptions;
using ParcelLogistics.SKS.Package.DataAccess.Mock;
using ParcelLogistics.SKS.Package.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Tests
{
    public class ExportImportLogicTests
    {
        private readonly IMapper _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();

        [Test]
        public void ImportWarehouse_Successed()
        {
            var ExportImportLogic = new ExportImportLogic(new MockWarehouseRepository(), _mapper);

        }

        [Test]
        public void ImportWarehouse_InvalidRootWarehouse_InvalidCode()
        {
            var ExportImportLogic = new ExportImportLogic(new MockWarehouseRepository(), _mapper);

            var wh = MockBuilder.CreateValidWarehouse();
            wh.Code = "*";

            Assert.Throws<BlException>(() => ExportImportLogic.ImportWarehouses(wh));   
        }

        [Test]
        public void ExportWarehouse_Successed()
        {
            var ExportImportLogic = new ExportImportLogic(new MockWarehouseRepository(), _mapper);

            //var warehouse = ExportImportLogic.ExportWarehouses();

            //Assert.NotNull(warehouse);
        }
    }
}
