using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities
{
    /// <summary>
    /// Defines the <see cref="Parcel" />
    /// </summary>
    public class Parcel
    {
        /// <summary>
        /// State of the parcel.
        /// </summary>
        /// <value>State of the parcel.</value>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum StateEnum
        {
            /// <summary>
            /// Enum InTransportEnum for InTransport
            /// </summary>
            [EnumMember(Value = "InTransport")]
            InTransportEnum = 0,
            /// <summary>
            /// Enum InTruckDeliveryEnum for InTruckDelivery
            /// </summary>
            [EnumMember(Value = "InTruckDelivery")]
            InTruckDeliveryEnum = 1,
            /// <summary>
            /// Enum DeliveredEnum for Delivered
            /// </summary>
            [EnumMember(Value = "Delivered")]
            DeliveredEnum = 2
        }

        /// <summary>
        /// Gets or sets the Weight
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Gets or sets the Receipient
        /// </summary>
        public Receipient Receipient { get; set; }

        /// <summary>
        /// Gets or sets the Sender
        /// </summary>
        public Receipient Sender { get; set; }

        /// <summary>
        /// Gets or sets the TrackingId
        /// </summary>
        public string TrackingId { get; set; }

        /// <summary>
        /// Gets or sets the State
        /// </summary>
        public StateEnum? State { get; set; }

        /// <summary>
        /// Gets or sets the VisitedHops
        /// </summary>
        public List<HopArrival> VisitedHops { get; set; }

        /// <summary>
        /// Gets or sets the FutureHops
        /// </summary>
        public List<HopArrival> FutureHops { get; set; }
    }
}
