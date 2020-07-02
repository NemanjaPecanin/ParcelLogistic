using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ParcelLogistics.SKS.Package.Services.DTOs
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Transferwarehouse : Hop
    {
        [DataMember(Name = "regionGeoJson")]
        public string RegionGeoJson { get; set; }

        [DataMember(Name = "logisticsPartner")]
        public string LogisticsPartner { get; set; }

        [DataMember(Name = "logisticsPartnerUrl")]
        public string LogisticsPartnerUrl { get; set; }

    }
}
