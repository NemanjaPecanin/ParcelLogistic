using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using ParcelLogistics.SKS.Package.Services.Attributes;

using Microsoft.AspNetCore.Authorization;
using ParcelLogistics.SKS.Package.Services.DTOs;
using AutoMapper;
using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;

namespace ParcelLogistics.SKS.Package.Services.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class LogisticsPartnerApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITrackingLogic _trackingLogic;

        /// <summary>
        /// 
        /// </summary>
        public LogisticsPartnerApiController(IMapper mapper, ITrackingLogic trackingLogic)
        {
            _mapper = mapper;
            _trackingLogic = trackingLogic;
        }

        /// <summary>
        /// Transfer an existing parcel from the service of a logistics partner. 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="trackingId">The tracking ID of the parcel. E.g. PYJRB4HZ6 </param>
        /// <response code="200">Successfully transitioned the parcel</response>
        /// <response code="400">The operation failed due to an error.</response>
        [HttpPost]
        [Route("/api/parcel/{trackingId}")]
        [ValidateModelState]
        [SwaggerOperation("TransitionParcel")]
        [SwaggerResponse(statusCode: 200, type: typeof(NewParcelInfo), description: "Successfully transitioned the parcel")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult TransitionParcel([FromBody]Parcel body, [FromRoute][Required][RegularExpression("^[A-Z0-9]{9}$")]string trackingId)
        {
            if (trackingId == null)
            {
                Error error = new Error();
                error.ErrorMessage = "No parcel with this Tracking ID found";

                return new BadRequestObjectResult(error);

            }
            else if (string.IsNullOrWhiteSpace(trackingId))
            {
                Error error = new Error();
                error.ErrorMessage = "No parcel with empty Tracking ID found";

                return new BadRequestObjectResult(error);
            }
            else
            {
                BusinessLogic.Entities.Parcel blParcel = _mapper.Map<DTOs.Parcel, BusinessLogic.Entities.Parcel>(body);
                return Ok(_trackingLogic.submitParcel(blParcel));
            }
        }
    }
}
