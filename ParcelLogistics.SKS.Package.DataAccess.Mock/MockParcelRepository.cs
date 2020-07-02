using ParcelLogistics.SKS.Package.DataAccess.Entities;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Mock
{
    public class MockParcelRepository : IParcelRepository
    {
        private readonly Dictionary<int, Parcel> _dicParcels = new Dictionary<int, Parcel>();
        private int _id = 0;

        public int Create(Parcel p)
        {
            p.ID = _id++;
            _dicParcels.Add(_id, p);
            return p.ID;
        }

        public void Delete(int id)
        {
            _dicParcels.Remove(id);
        }

        public Parcel GetById(int id)
        {
            return _dicParcels.Values.Single(p => p.ID == id);            
        }

        public Parcel GetByTrackingId(string id)
        {
           return _dicParcels.Values.Single(p => p.TrackingId == id);
        }

        public void Update(Parcel p)
        {
            _dicParcels[p.ID] = p;
        }
    }
}
