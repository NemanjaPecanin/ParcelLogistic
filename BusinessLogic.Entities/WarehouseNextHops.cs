namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="WarehouseNextHops" />
    /// </summary>
    public class WarehouseNextHops
    {
        /// <summary>
        /// Gets or Sets TraveltimeMins
        /// </summary>
        public int? TraveltimeMins { get; set; }

        /// <summary>
        /// Gets or Sets Hop
        /// </summary>
        public Hop Hop { get; set; }
    }
}
