using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Entities
{
    public class Truck : Hop
    {
        [Required]
        public Geometry RegionGeometry { get; set; }

        [Required]
        public string NumberPlate { get; set; }
    }
}
