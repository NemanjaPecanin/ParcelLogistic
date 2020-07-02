using System;
using System.Collections.Generic;
using System.Text;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Interfaces
{
    public interface ITrackingLogic
    {
        Parcel trackParcel(string TrackingId);
        Parcel submitParcel(Parcel parcel);
        void transitionParcel(string TrackingId, string code);
        void ReportParcelDelivery(string trackingId);
    }
}
