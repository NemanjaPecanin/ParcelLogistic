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
    public partial class WarehouseNextHops
    { 
        /// <summary>
        /// Gets or Sets TraveltimeMins
        /// </summary>
        [Required]
        [DataMember(Name="traveltimeMins")]
        public int? TraveltimeMins { get; set; }

        /// <summary>
        /// Gets or Sets Hop
        /// </summary>
        [Required]
        [DataMember(Name="hop")]
        public Hop Hop { get; set; }
    }
}
