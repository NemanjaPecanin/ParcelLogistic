using ParcelLogistics.SKS.Package.DataAccess.Sql;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.DataAccess.Entities;
using ParcelLogistics.SKS.Package.DataAccess.Sql.Exceptions;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;

namespace ParcelLogistics.SKS.Package.DataAccess.Tests
{
    class ParcelRepositoryTests
    {
        private SqlParcelRepository dal;

        [SetUp]
        public void Setup()
        {
            string testName = "pl-db";
            DbContextOptions<SqlContext> options = new DbContextOptionsBuilder<SqlContext>().UseInMemoryDatabase(databaseName: testName).Options;
            dal = new SqlParcelRepository(new SqlContext(options));

        }

        [Test]
        public void GetById_ParcelId_Parcel()
        {
            var parcel = new Parcel();
            var id = dal.Create(parcel);

            var result = dal.GetById(id);

            Assert.NotNull(result);
        }

        [Test]
        public void GetByTrackingId_EmptyId_ThrowsException()
        {
            var trackingId = "";

            Assert.Throws<DALException>(() => dal.GetByTrackingId(trackingId));
        }

        [Test]
        public void Update_Weight_UpdatedWeight10()
        {
            var parcel = new Parcel();
            parcel.Weight = 5;
            var id = dal.Create(parcel);

            parcel.Weight = 10;
            dal.Update(parcel);

            var result = dal.GetById(id);

            Assert.AreEqual(10, result.Weight);
            Assert.AreNotEqual(11, result.Weight);
        }

        [Test]
        public void Update_ParcelNull_ThrowsException()
        {
            var parcel = new Parcel();
            parcel = null;

            Assert.Throws<DALException>(() => dal.Update(parcel));
        }

        [Test]
        public void deleteParcel()
        {
            Parcel parcel = new Parcel();
            int id = dal.Create(parcel);

            dal.Delete(id);
            IEnumerable<Parcel> parcels = dal.GetAll();

            Assert.AreNotEqual(0, id);
        }

        [Test]
        public void deleteParcel_NegativId_ThrowsException()
        {
            Parcel parcel = new Parcel();
            int id = -20;

            Assert.Throws<DALException>(() => dal.Delete(id));
        }

        [Test]
        public void addParcel()
        {
            Parcel parcel = new Parcel();
            int id = dal.Create(parcel);

            Assert.AreNotEqual(0, id);
        }

        [Test]
        public void addParcel_ParcelNull_ThrowsException()
        {
            Parcel parcel = new Parcel();
            parcel = null;

            Assert.Throws<DALException>(() => dal.Create(parcel));
        }

        [Test]
        public void GetAllParcels_NotNull()
        {
            IEnumerable<Parcel> parcels = dal.GetAll();

            Assert.NotNull(parcels);
        }

        [Test]
        public void dbConnection_Successful()
        {
        }
    }
}
