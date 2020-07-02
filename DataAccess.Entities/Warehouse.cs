using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Entities
{
    public class Warehouse : Hop
    {
        public List<WarehouseNextHops> NextHops { get; set; }
    }
}
