using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.DataAccess.Entities;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using ParcelLogistics.SKS.Package.DataAccess.Sql;
using ParcelLogistics.SKS.Package.DataAccess.Sql.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ParcelLogistics.SKS.Package.DataAccess.Tests
{
    class TrackingPointRepositoryTests
    {

        private SqlTrackingPointRepository dal;

        [SetUp]
        public void Setup()
        {
            string testName = "pl-db";
            DbContextOptions<SqlContext> options = new DbContextOptionsBuilder<SqlContext>().UseInMemoryDatabase(databaseName: testName).Options;
            dal = new SqlTrackingPointRepository(new SqlContext(options));
        }

        [Test]
        public void CreateTruck_Succeeded()
        {
            Truck hop = new Truck() { HopType = "Truck" };
            dal.Create(hop);

            IEnumerable<Truck> trucks = dal.GetAllTrucks();
            Assert.NotZero(trucks.Count());
            Truck truck = trucks.FirstOrDefault();
            Assert.NotNull(truck);
            Assert.AreEqual("Truck", truck.HopType);
        }

        [Test]
        public void Create_HopNull_ThrowsException()
        {
            Hop hop = new Hop();
            hop = null;

            Assert.Throws<DALException>(() => dal.Create(hop));
        }

        [Test]
        public void GetAllHops_Succeeded()
        {
            Truck truck = new Truck() { HopType = "Truck" };
            dal.Create(truck);

            Warehouse wh = new Warehouse() { HopType = "Warehouse" };
            dal.Create(wh);

            Transferwarehouse transfer = new Transferwarehouse() { HopType = "Transferwarehouse" };
            dal.Create(transfer);

            IEnumerable<Hop> hops = dal.GetAllHops();
            Assert.NotZero(hops.Count());
            //Assert.AreEqual(hops.Count(), 3);
            Assert.AreEqual(hops.Count(), 4);
        }

        [Test]
        public void GetAllTrucks_Successed()
        {
            Truck hop = new Truck() { HopType = "Truck" };
            dal.Create(hop);

            IEnumerable<Truck> trucks = dal.GetAllTrucks();
            Assert.NotZero(trucks.Count());
        }

        [Test]
        public void Delete()
        {
            Assert.Throws<DALException>(() => dal.Delete(-99));
        }
    }
}
