using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using ParcelLogistics.SKS.Package.DataAccess.Entities;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using ParcelLogistics.SKS.Package.DataAccess.Sql.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Sql
{
    public class SqlWarehouseRepository : IWarehouseRepository
    {
        private readonly SqlContext _sqlContext;

        public SqlWarehouseRepository(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public void Clear()
        {
            _sqlContext.Database.EnsureDeleted();
            _sqlContext.Database.EnsureCreated();
        }

        public int Create(Hop hop)
        {
            if(hop == null)
            {
                throw new DALException("Hop can not be null");
            }

            Clear();
            _sqlContext.Hops.Add(hop);
            _sqlContext.SaveChanges();
            return hop.ID;
        }

        public void Delete(int id)
        {
            if(id < 0)
            {
                throw new DALException("Id can not have negative value");
            }

            _sqlContext.Remove(_sqlContext.Hops.Single(x => x.ID == id));
            _sqlContext.SaveChanges();
        }

        public Hop GetByID(int id)
        {
            if (id < 0)
            {
                throw new DALException("Id can not have negative value.");
            }   

            return _sqlContext.Hops.Single(x => x.ID == id);  
        }

        public void Update(Hop hop)
        {
            if(hop == null)
            {
                throw new DALException("Hop can not be null");
            }

            var h = GetByID(hop.ID);
            _sqlContext.Entry(h).CurrentValues.SetValues(hop);
            _sqlContext.SaveChanges();
        }

        public Warehouse GetRootWarehouse()
        {
            _sqlContext.Warehouses.Load();
            _sqlContext.NextHops.Load();
            _sqlContext.Trucks.Load();
            _sqlContext.Transferwarehouses.Load();

            return _sqlContext.Hops.OfType<Warehouse>().FirstOrDefault(x => x.ID == 1);
        }

        public Hop GetByCoordinates(double longitude, double latitude)
        {
            var factory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var cord = new Coordinate(longitude, latitude);
            var point = factory.CreatePoint(cord);

            return _sqlContext.Trucks.FirstOrDefault(x => x.RegionGeometry.Contains(point)) ?? (Hop)_sqlContext.Transferwarehouses.FirstOrDefault(x => x.RegionGeometry.Contains(point)); // First if more
        }

        public IEnumerable<Warehouse> GetAllWarehouses()
        {
            _sqlContext.Warehouses.Load();
            _sqlContext.NextHops.Load();
            _sqlContext.Trucks.Load();
            _sqlContext.Transferwarehouses.Load();

            return _sqlContext.Warehouses.Include(wh => wh.NextHops);
        }

        public Hop GetByCode(string code)
        {
            return _sqlContext.Hops.FirstOrDefault(x => x.Code == code);
        }

    }
}
