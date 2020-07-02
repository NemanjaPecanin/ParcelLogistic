//using ParcelLogistics.SKS.Package.DataAccess.Entities;
//using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace ParcelLogistics.SKS.Package.DataAccess.Mock
//{
//    public class MockTrackingPointRepository : ITrackingPointRepository
//    {
//        private int _tpId;
//        private readonly Dictionary<int, TrackingPoint> _dictp = new Dictionary<int, TrackingPoint>();

//        public void Create(Hop hop)
//        {
//            hop.ID = _tpId++;
//            _dictp.Add(_tpId, hop);
//        }

//        public List<Truck> GetAllTrucks()
//        {
//            return _dictp.Values;
//        }

//        public Truck GetTruck(string code)
//        {
//            throw new NotImplementedException();
//        }

//        public Warehouse GetWarehouse(string code)
//        {
//            throw new NotImplementedException();
//        }

//        public Warehouse GetWarehouseByTruck(string TruckCode)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
