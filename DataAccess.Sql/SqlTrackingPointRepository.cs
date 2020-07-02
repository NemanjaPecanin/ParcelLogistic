using ParcelLogistics.SKS.Package.DataAccess.Entities;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using ParcelLogistics.SKS.Package.DataAccess.Sql.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Sql
{
    public class SqlTrackingPointRepository : ITrackingPointRepository
    {
        private readonly SqlContext _dbContext;

        public SqlTrackingPointRepository(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Hop hop)
        {
            if(hop == null)
            {
                throw new DALException("Hop can not be null.");
            }

            _dbContext.Hops.Add(hop);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            try
            {
                if(id < 0)
                {
                    throw new DALException("Id can not have negative value.");
                }

                _dbContext.Remove(_dbContext.Hops.Single(x => x.ID == id));
                _dbContext.SaveChanges();
            }
            catch (InvalidOperationException exc)
            {
                throw new DALException("Invalid Operation Exception", exc);
            }
        }

        public IEnumerable<Truck> GetAllTrucks()
        {
            return _dbContext.Hops.Where(x => x.HopType == "Truck").Cast<Truck>();
        }

        public IEnumerable<Hop> GetAllHops()
        {
            return _dbContext.Hops;
        }

        public Truck GetTruck(string code)
        {
            return _dbContext.Trucks.FirstOrDefault(x => x.Code == code);
        }

        public Warehouse GetWarehouse(string code)
        {
            return _dbContext.Warehouses.FirstOrDefault(x => x.Code == code);
        }

        public Warehouse GetWarehouseByTruck(string truckCode)
        {
            throw new NotImplementedException();
        }
    }
}
