using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

using ParcelLogistics.SKS.Package.Services.DTOs;
using ParcelLogistics.SKS.Package.BusinessLogic;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities;
using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;
using ParcelLogistics.SKS.Package.Services.Attributes;

using AutoMapper.Mappers;
using AutoMapper;
using Warehouse = ParcelLogistics.SKS.Package.Services.DTOs.Warehouse;
using ParcelLogistics.SKS.Package.Services.Mapper;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities.Exceptions;

namespace ParcelLogistics.SKS.Package.Services.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class WarehouseManagementApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExportImportLogic _exportImportLogic;

        /// <summary>
        /// 
        /// </summary>
        public WarehouseManagementApiController(IMapper mapper, IExportImportLogic exportImportLogic)
        {
            _mapper = mapper;
            _exportImportLogic = exportImportLogic;
        }

        /// <summary>
        /// Exports the hierarchy of Warehouse and Truck objects. 
        /// </summary>
        /// <response code="200">Successful response</response>
        /// <response code="400">An error occurred loading.</response>
        /// <response code="404">No hierarchy loaded yet.</response>
        [HttpGet]
        [Route("/api/warehouse")]
        [ValidateModelState]
        [SwaggerOperation("ExportWarehouses")]
        [SwaggerResponse(statusCode: 200, type: typeof(Warehouse), description: "Successful response")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "An error occurred loading.")]
        public virtual IActionResult ExportWarehouses()
        {
            //return new ObjectResult(_mapper.Map<BusinessLogic.Entities.Warehouse>(_exportImportLogic.ExportWarehouses()));

            var wh = _exportImportLogic.ExportWarehouses();
            if(wh == null)
            {
                Error error = new Error();
                error.ErrorMessage = "No hierarchy found";

                return new BadRequestObjectResult(error);
            }

            var warehouse = _mapper.Map<Warehouse>(wh);
            return Ok(warehouse);
        }

        /// <summary>
        /// Imports a hierarchy of Warehouse and Truck objects. 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Successfully loaded.</response>
        /// <response code="400">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/api/warehouse")]
        [ValidateModelState]
        [SwaggerOperation("ImportWarehouses")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult ImportWarehouses([FromBody]Warehouse body)
        {
   
                if (body == null)
                {
                    return new BadRequestResult();
                }
                else
                {
                    BusinessLogic.Entities.Warehouse warehouse = _mapper.Map<BusinessLogic.Entities.Warehouse>(body);
                    _exportImportLogic.ImportWarehouses(warehouse);
                    return Ok(warehouse);
                }

           
        }
    }
}
