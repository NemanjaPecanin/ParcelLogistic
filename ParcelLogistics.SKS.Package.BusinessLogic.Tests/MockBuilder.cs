using FizzWare.NBuilder;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Tests
{
    public static class MockBuilder
    {
        public static Parcel NotNgBuilderParcel()
        {
            return new Parcel()
            {
                Weight = 10.0f,
                Sender = new Receipient()
                {
                    Name = "Nemanja",
                    Street = "Koppstrasse 18",
                    PostalCode = "A-1160",
                    City = "Vienna",
                    Country = "Austria"
                },
                Receipient = new Receipient()
                {
                    Name = "Ognjen",
                    Street = "Auf der Schmelz 12",
                    PostalCode = "A-1150",
                    City = "Vienna",
                    Country = "Austria"
                }
            };
        }

        public static Warehouse CreateValidWarehouse()
        {
            return Builder<Warehouse>.CreateNew()
                .With(x => x.Code = "FRA01")
                .With(x => x.Description = "Main Warehouse Germany")
                .With(x => x.ProcessingDelayMins = 198)
                .With(x => x.HopType = "Truck")
                .Build();
        }

        public static Parcel CreateValidParcel()
        {
            return Builder<Parcel>.CreateNew()
                .With(x => x.TrackingId = "123456789")
                .With(x => x.Weight = 3.0f)
                .With(x => x.Sender = new Receipient(
                    ))
                .With(x => x.Receipient = new Receipient())
                .Build();
        }
    }
}
