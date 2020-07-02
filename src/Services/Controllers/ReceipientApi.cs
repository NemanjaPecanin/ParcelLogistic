namespace ParcelLogistics.SKS.Package.Services.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using ParcelLogistics.SKS.Package.BusinessLogic;
    using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;
    using ParcelLogistics.SKS.Package.Services.Attributes;
    using ParcelLogistics.SKS.Package.Services.DTOs;
    using Swashbuckle.AspNetCore.Annotations;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="ReceipientApiController" />
    /// </summary>
    [ApiController]
    public class ReceipientApiController : ControllerBase
    {
        /// <summary>
        /// Defines the _mapper
        /// </summary>
        private readonly IMapper _mapper;
        private readonly ITrackingLogic _trackingLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceipientApiController"/> class.
        /// </summary>
        /// <param name="logic"></param>
        /// <param name="mapper">The mapper<see cref="IMapper"/></param>
        public ReceipientApiController(ITrackingLogic logic, IMapper mapper)
        {
            _mapper = mapper;
            _trackingLogic = logic;
        }

        /// <summary>
        /// Find the latest tracking state of a parcel by its tracking ID.
        /// </summary>
        /// <param name="trackingId">The tracking ID of the parcel. E.g. PYJRB4HZ6 </param>
        /// <returns>The <see cref="IActionResult"/></returns>
        [EnableCors("CorsPolicy")]
        [HttpGet]
        [Route("/api/parcel/{trackingId}")]
        [ValidateModelState]
        [SwaggerOperation("TrackParcel")]
        [SwaggerResponse(statusCode: 200, type: typeof(TrackingInformation), description: "Parcel exists, here&#x27;s the tracking information.")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error), description: "The operation failed due to an error.")]
        public virtual IActionResult TrackParcel([FromRoute][Required][RegularExpression("^[A-Z0-9]{9}$")]string trackingId)
        {

            var blParcel = _trackingLogic.trackParcel(trackingId);

            if (blParcel == null)
            {
                Error error = new Error();
                error.ErrorMessage = "No parcel with this Tracking ID found";

                return new NotFoundObjectResult(error);
            }
            else
            {
                return Ok(_mapper.Map<TrackingInformation>(blParcel));
            }
        }
    }
}
