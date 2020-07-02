using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities
{
    public class Truck : Hop
    {
        public string RegionGeoJson { get; set; }
        public string NumberPlate { get; set; }
    }
}
