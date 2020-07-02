using ParcelLogistics.SKS.Package.DataAccess.Entities;
using System.Collections.Generic;

namespace ParcelLogistics.SKS.Package.DataAccess.Interfaces
{

    public interface IParcelRepository
    {
        int Create(Parcel p);
        void Update(Parcel p);
        void Delete(int id);
        Parcel GetByTrackingId(string id);
        Parcel GetById(int id);
    }
}
