using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Entities
{
    public class Transferwarehouse : Hop
    {
        [Required]
        [Column(TypeName = "Geometry")]
        public Geometry RegionGeometry { get; set; }

        [Required]
        public string LogisticsPartner { get; set; }

        [Required]
        public string LogisticsPartnerUrl { get; set; }
    }
}
