namespace ParcelLogistics.SKS.Package.BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Warehouse" />
    /// </summary>
    public class Warehouse : Hop
    {
        public IEnumerable<WarehouseNextHops> NextHops { get; set; }
    }
}
