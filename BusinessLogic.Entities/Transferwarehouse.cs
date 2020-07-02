using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities
{
    public class Transferwarehouse : Hop
    {
        public string RegionGeoJson { get; set; }
        public string LogisticsPartner { get; set; }
        public string LogisticsPartnerUrl { get; set; }
    }
}
