using System;
using System.Collections.Generic;
using System.Text;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities;
using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;
using Newtonsoft.Json;
using AutoMapper;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities.Validators;
using ParcelLogistics.SKS.Package.ServiceAgents.Interfaces;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities.Exceptions;

namespace ParcelLogistics.SKS.Package.BusinessLogic
{
    public class ExportImportLogic : IExportImportLogic
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public ExportImportLogic(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public Warehouse ExportWarehouses()
        {
            var root = _warehouseRepository.GetRootWarehouse();
            return _mapper.Map<Warehouse>(root);   
        }

        public void ImportWarehouses(Warehouse warehouse)
        {
            if(new WarehouseValidator().Validate(warehouse).IsValid)
            {
                _warehouseRepository.Create(_mapper.Map<DataAccess.Entities.Hop>(warehouse));
            }
            else
            {
                throw new BlException("invalid Warehouse format.");
            }
        }
    }
}
