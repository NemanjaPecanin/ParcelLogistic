using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace ParcelLogistics.SKS.Package.DataAccess.Entities
{

    public class Parcel
    {

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum StateEnum
        {

            [EnumMember(Value = "InTransport")]
            InTransportEnum = 0,
            [EnumMember(Value = "InTruckDelivery")]
            InTruckDeliveryEnum = 1,
            [EnumMember(Value = "Delivered")]
            DeliveredEnum = 2
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int ID { get; set; }

        public float Weight { get; set; }

        public Receipient Sender { get; set; }

        public Receipient Receipient { get; set; }

        [Required]
        public string TrackingId { get; set; }

        public StateEnum? State { get; set; }

        public List<HopArrival> VisitedHops { get; set; }

        public List<HopArrival> FutureHops { get; set; }
    }
}
