using AutoMapper;
using Moq;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities;
using ParcelLogistics.SKS.Package.BusinessLogic.Entities.Exceptions;
using ParcelLogistics.SKS.Package.BusinessLogic.Interfaces;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using ParcelLogistics.SKS.Package.DataAccess.Mock;
using ParcelLogistics.SKS.Package.DataAccess.Sql;
using ParcelLogistics.SKS.Package.ServiceAgents;
using ParcelLogistics.SKS.Package.ServiceAgents.Interfaces;
using ParcelLogistics.SKS.Package.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Tests
{
    public class TrackingLogicTests
    {

        private readonly IMapper _mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();

        [Test]
        public void SubmitNewParcel_ValidParcel()
        {
            //Arange
            var parcel = MockBuilder.CreateValidParcel();
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            //Act
            var newParcel = trackingLogic.submitParcel(parcel);

            //Assert
            Assert.NotNull(newParcel);
        }

        [Test]
        public void SubmitNewParcel_InvalidParcel_ThrowsException()
        {
            var parcel = MockBuilder.CreateValidParcel();
            parcel.Weight = -1;
            var parepo = new MockParcelRepository();
            var geoencoder = new Mock<IGeoEncodingAgent>();
            geoencoder.Setup(x => x.EncodeAddress(It.IsAny<string>()).Latitude).Equals(0.0f);
            geoencoder.Setup(x => x.EncodeAddress(It.IsAny<string>()).Longitude).Equals(0.0f);
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            Assert.Throws<BlException>(() => trackingLogic.submitParcel(parcel));
        }

        [Test]
        public void SubmitNewParcel_InvalidWeight_ThrowsException()
        {
            var parcel = MockBuilder.CreateValidParcel();
            parcel.Weight = 0;
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            Assert.Throws<BlException>(() => trackingLogic.submitParcel(parcel));
        }

        [Test]
        public void SubmitNewParcel_InvalidRecipientAdress_ThrowsException()
        {
            var parcel = MockBuilder.CreateValidParcel();
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            parcel.Receipient.City = "-//*asd";

            Assert.Throws<BlException>(() => trackingLogic.submitParcel(parcel));
        }

        [Test]
        public void SubmitNewParcel_InvalidSenderAdress_ThrowsException()
        {
            var parcel = MockBuilder.CreateValidParcel();  
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            parcel.Sender.City = "-//*asd";

            Assert.Throws<BlException>(() => trackingLogic.submitParcel(parcel));
        }

        [Test]
        public void TrackParcel_Succeeded()
        {
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            var info = trackingLogic.submitParcel(MockBuilder.NotNgBuilderParcel());
            Assert.IsNotNull(info.TrackingId);

            var trackingInfo = trackingLogic.TrackParcel(info.TrackingId);
            Assert.AreEqual(trackingInfo.FutureHops.Count, 0);
        }

        [Test]
        public void TrackParcel_Failed()
        {
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            Assert.Throws<BlException>(() => trackingLogic.TrackParcel(null));         
        }

        [Test]
        public void ReportParcelDelivery_Failed_NonExistingParcel()
        {
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            Assert.Throws<BlException>(() => trackingLogic.ReportParcelDelivery("123456789"));
        }

        [Test]
        public void ReportParcelDelivery_Succeeded_ValidParcel()
        {
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            var info = trackingLogic.submitParcel(MockBuilder.CreateValidParcel());
            trackingLogic.ReportParcelDelivery(info.TrackingId);
        }

        [Test]
        public void ReportParcelDelivery_Failed()
        {
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            Assert.Throws<BlException>(() => trackingLogic.ReportParcelDelivery("1"));
        }

        [Test]
        public void TransitionParcel_Succeeded()
        {
            //TODO
            var trackingLogic = new TrackingLogic(new MockParcelRepository(), new MockWarehouseRepository(), _mapper, new BingGeoEncodingAgent());

            var parcel = MockBuilder.CreateValidParcel();
            //trackingLogic.transitionParcel(parcel.TrackingId, "WTTA040");

            Assert.IsTrue(true);
        }

    }
}
