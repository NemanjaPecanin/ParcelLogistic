using System;
using System.ComponentModel.DataAnnotations;

namespace ParcelLogistics.SKS.Package.DataAccess.Entities
{
    public class HopArrival
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Code { get; set; }

        public DateTime? TimeStamp { get; set; }
    }
}
