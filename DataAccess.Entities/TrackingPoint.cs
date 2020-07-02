using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParcelLogistics.SKS.Package.DataAccess.Entities
{
    public class TrackingPoint
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        [Column(TypeName = "decimal(16, 14)")]
        public decimal? Latitude { get; set; }
        [Column(TypeName = "decimal(16, 14)")]
        public decimal? Longitude { get; set; }
        public decimal? Duration { get; set; }
    }
}
