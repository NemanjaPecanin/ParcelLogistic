using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Entities
{
    public class WarehouseNextHops
    {
        [Required]
        [Key]
        public int ID { get; set; }

        public int? TraveltimeMins { get; set; }

        [Required]
        public Hop Hop { get; set; }
    }
}
