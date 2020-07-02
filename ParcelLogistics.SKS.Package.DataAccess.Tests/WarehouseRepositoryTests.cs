using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NUnit.Framework;
using ParcelLogistics.SKS.Package.DataAccess.Entities;
using ParcelLogistics.SKS.Package.DataAccess.Sql;
using ParcelLogistics.SKS.Package.DataAccess.Sql.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Tests
{
    public class WarehouseRepositoryTests
    {
        private SqlWarehouseRepository dal;

        [SetUp]
        public void Setup()
        {
            string testName = "pl-db";
            DbContextOptions<SqlContext> options = new DbContextOptionsBuilder<SqlContext>().UseInMemoryDatabase(databaseName: testName).Options;
            dal = new SqlWarehouseRepository(new SqlContext(options));
        }

        [Test]
        public void addHopArrival()
        {
            Hop hop = new Hop();

            int id = dal.Create(hop);

            Assert.AreNotEqual(0, id);
        }

        [Test]
        public void addHopArrival_HopNull_ThrowsException()
        {
            Hop hop = new Hop();
            hop = null;

            Assert.Throws<DALException>(() => dal.Create(hop));
        }

        [Test]
        public void deleteHopArrival_IdNegativeValue_ThrowsException()
        {
            var id = -99;
            Assert.Throws<DALException>(() => dal.Delete(id));
        }

        [Test]
        public void getById()
        {
            Hop hop = new Hop();
            int id = dal.Create(hop);
            var result = dal.GetByID(id);

            Assert.AreNotEqual(0, id);
            Assert.IsInstanceOf<Hop>(result);
        }

        [Test]
        public void getById_IdNegativeValue_ThrowsException()
        {
            var id = -99;
            Assert.Throws<DALException>(() => dal.GetByID(id));
        }

        [Test]
        public void Update_HopNull_ThrowsException()
        {
            Hop hop = new Hop();
            hop = null;

            Assert.Throws<DALException>(() => dal.Update(hop));
        }

        [Test]
        public void GetRootWarehouse()
        {
            var result = dal.GetRootWarehouse();

            //Assert.IsNotNull(result);
            //Assert.IsInstanceOf<Warehouse>(result);
        }

        [Test]
        public void GetAllWarehouses()
        {
            var result = dal.GetAllWarehouses();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetHop_ByCoordinates_Succeeded()
        {
            var testTruck = new Truck()
            {
                Code = "SPEED12",
                Description = "Speedwagon",
                HopType = "Truck",
                NumberPlate = "EGYPT-6969",
                RegionGeometry = new Polygon(new LinearRing(new[]{
                    new Coordinate(16.511978,47.8466967),
                    new Coordinate(16.5109296,47.8477443),
                    new Coordinate(16.510713,47.8476152),
                    new Coordinate(16.511978,47.8466967)
                }))
            };
            dal.Create(testTruck);
           
            var hop = dal.GetByCoordinates(16.51089, 47.84767);

            Assert.NotNull(hop);
        }


    }
}
