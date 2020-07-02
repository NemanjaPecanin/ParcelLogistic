using ParcelLogistics.SKS.Package.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Interfaces
{
    public interface IHopArrivalRepository
    {
        int Create(Parcel p, TrackingPoint tp);
    }
}
