using NUnit.Framework;
using ParcelLogistics.SKS.Package.ServiceAgents;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.Tests
{
    public class GeoEncodingTests
    {
        private readonly BingGeoEncodingAgent _encoder = new BingGeoEncodingAgent();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EncodeAddress_Succeeded()
        {
            var location = _encoder.EncodeAddress("Lorenz-Müller-Gasse 2, Wien");
            Assert.AreEqual(16.368785036175378d, location.Longitude);
            Assert.AreEqual(48.244056548191942d, location.Latitude);
        }
    }
}
