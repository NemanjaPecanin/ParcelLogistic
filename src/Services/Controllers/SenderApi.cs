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
using ParcelLogistics.SKS.Package.BusinessLogic.Entities.Exceptions;
using Microsoft.AspNetCore.Cors;

namespace ParcelLogistics.SKS.Package.Services.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class SenderApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITrackingLogic _trackingLogic;

        /// <summary>
        /// 
        /// </summary>
        public SenderApiController(IMapper mapper, ITrackingLogic trackingLogic)
        {
            _mapper = mapper;
            _trackingLogic = trackingLogic;
        }

        /// <summary>
        /// Submit a new parcel to the logistics service. 
        /// </summary>
        /// <param name="parcel"></param>
        /// <response code="200">Successfully submitted the new parcel</response>
        /// <response code="400">The operation failed due to an error.</response
        [EnableCors("CorsPolicy")]
        [HttpPost]
        [Route("/api/parcel")]
        [ValidateModelState]
        [SwaggerOperation("SubmitParcel")]
        [SwaggerResponse(statusCode: 200, type: typeof(NewParcelInfo), description: "Successfully submitted the new parcel")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult SubmitParcel([FromBody]DTOs.Parcel parcel)
        {
          
                if (parcel == null)
                {
                    Error error = new Error();
                    error.ErrorMessage = "No parcel with this Tracking ID found";

                    return new BadRequestObjectResult(error);
                }
                else
                {
                    var blParcel = _trackingLogic.submitParcel(_mapper.Map<BusinessLogic.Entities.Parcel>(parcel));
                    return Ok(new NewParcelInfo() { TrackingId = blParcel.TrackingId });
                } 
        }
    }
}
