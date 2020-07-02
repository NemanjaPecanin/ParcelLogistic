using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Entities
{
    public class Hop
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        public string HopType { get; set; }

        [Required]
        public string Code { get; set; }

        public string Description { get; set; }

        [Required]
        public int? ProcessingDelayMins { get; set; }
    }
}
