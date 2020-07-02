using NetTopologySuite;
using NetTopologySuite.Geometries;
using ParcelLogistics.SKS.Package.DataAccess.Entities;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Mock
{
    public class MockWarehouseRepository : IWarehouseRepository
    {
        private int _hopId;
        private readonly Dictionary<int, Hop> _dicHops = new Dictionary<int, Hop>();

        public void Clear()
        {
            _dicHops.Clear();
        }

        public int Create(Hop hop)
        {
            hop.ID = _hopId++;
            _dicHops.Add(_hopId, hop);
            if (hop.GetType().IsAssignableFrom(typeof(Warehouse)))
            {
                var wh = (Warehouse)hop;
                foreach (var nh in wh.NextHops)
                {
                    Create(nh.Hop);
                }
            }
            return hop.ID;
        }

        public void Delete(int id)
        {
            _dicHops.Remove(id);
        }

        public IEnumerable<Warehouse> GetAllWarehouses()
        {
            return _dicHops.Values.Where(x => x.HopType == "Warehouse").Cast<Warehouse>();
        }

        public Hop GetByCode(string code)
        {
            return _dicHops.Values.Single(x => x.Code == code);
        }

        public Hop GetByCoordinates(double longitude, double latitude)
        {
            var factory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var cord = new Coordinate(longitude, latitude);
            var point = factory.CreatePoint(cord);

            return _dicHops.Values.Where(x => x.HopType == "Truck").Cast<Truck>().FirstOrDefault(x => x.RegionGeometry.Contains(point)) ??
                      (Hop)_dicHops.Values.Where(x => x.HopType == "Transferwarehouse").Cast<Transferwarehouse>().FirstOrDefault(x => x.RegionGeometry.Contains(point));
        }

        public Warehouse GetRootWarehouse()
        {
            return _dicHops[1] as Warehouse;
        }

        public void Update(Hop hop)
        {
            _dicHops[hop.ID] = hop;
        }
    }
}
