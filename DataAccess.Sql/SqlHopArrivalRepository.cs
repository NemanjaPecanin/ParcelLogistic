using System;
using System.Collections.Generic;
using System.Text;
using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using ParcelLogistics.SKS.Package.DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParcelLogistics.SKS.Package.DataAccess.Sql.Exceptions;

namespace ParcelLogistics.SKS.Package.DataAccess.Sql
{
    public class SqlHopArrivalRepository : IHopArrivalRepository
    {
        private readonly SqlContext _dbContext;

        public SqlHopArrivalRepository(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Parcel p, TrackingPoint tp)
        {
            if(p == null || tp == null)
            {
                throw new DALException("Parcel or Tracking Point can not be null");
            }

            var hop = new HopArrival();
            hop.TimeStamp = DateTime.Now;

            _dbContext.HopArrivals.Add(hop);
            _dbContext.SaveChanges();
            return hop.ID;
        }
    }
}

