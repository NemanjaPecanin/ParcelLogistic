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
    public partial class Error
    { 
        /// <summary>
        /// The error message.
        /// </summary>
        /// <value>The error message.</value>
        [Required]
        [DataMember(Name="errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
