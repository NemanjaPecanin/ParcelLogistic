using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities
{
    public class Hop
    {
        /// <summary>
        /// Gets or Sets HopType
        /// </summary>
        public string HopType { get; set; }

        /// <summary>
        /// Unique CODE of the hop.
        /// </summary>
        /// <value>Unique CODE of the hop.</value>
        public string Code { get; set; }

        /// <summary>
        /// Description of the hop.
        /// </summary>
        /// <value>Description of the hop.</value>
        public string Description { get; set; }

        /// <summary>
        /// Delay processing takes on the hop.
        /// </summary>
        /// <value>Delay processing takes on the hop.</value>
        public int? ProcessingDelayMins { get; set; }

    }
}
