using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.DataAccess.Entities;
using ParcelLogistics.SKS.Package.DataAccess.Sql;
using ParcelLogistics.SKS.Package.DataAccess.Sql.Exceptions;

namespace ParcelLogistics.SKS.Package.DataAccess.Tests
{
    class HopArrivalRepositoryTests
    {

        private SqlHopArrivalRepository dal;

        [SetUp]
        public void Setup()
        {
            // string testName = TestContext.CurrentContext.Test.Name;

            string testName = "pl-db";
            DbContextOptions<SqlContext> options = new DbContextOptionsBuilder<SqlContext>().UseInMemoryDatabase(databaseName: testName).Options;
            dal = new SqlHopArrivalRepository(new SqlContext(options));
        }

        [Test]
        public void addHopArrival()
        {
            TrackingPoint tp = new TrackingPoint();
            Parcel p = new Parcel();

            int id = dal.Create(p, tp);

            Assert.AreNotEqual(0, id);
        }

        [Test]
        public void addHopArrival_ParcelOrTrackingPointNull_ThrowsException()
        {
            TrackingPoint tp = new TrackingPoint();
            Parcel p = new Parcel();
            p = null;

            Assert.Throws<DALException>(() => dal.Create(p, tp));
        }
    }
}
