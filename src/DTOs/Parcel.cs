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
    public partial class Parcel
    { 
        /// <summary>
        /// Gets or Sets Weight
        /// </summary>
        [Required]
        [DataMember(Name="weight")]
        public float? Weight { get; set; }

        /// <summary>
        /// Gets or Sets Receipient
        /// </summary>
        [Required]
        [DataMember(Name="receipient")]
        public Receipient Receipient { get; set; }

        /// <summary>
        /// Gets or Sets Sender
        /// </summary>
        [Required]
        [DataMember(Name="sender")]
        public Receipient Sender { get; set; }
    }
}
