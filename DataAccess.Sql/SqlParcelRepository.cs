using ParcelLogistics.SKS.Package.DataAccess.Interfaces;
using ParcelLogistics.SKS.Package.DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using ParcelLogistics.SKS.Package.DataAccess.Sql.Exceptions;

namespace ParcelLogistics.SKS.Package.DataAccess.Sql
{
    public class SqlParcelRepository : IParcelRepository
    {

        private readonly SqlContext _dbContext;

        public SqlParcelRepository(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(Parcel p)
        {
            if(p == null)
            {
                throw new DALException("Parcel can not be null.");
            }

            _dbContext.Parcels.Add(p);
            _dbContext.SaveChanges();
            return p.ID;
        }

        public void Delete(int id)
        {
            if (id < 0)
            {
                throw new DALException("Id can not have negativ value.");
            }

            var parcel = _dbContext.Parcels.FirstOrDefault(x => x.ID == id);
            _dbContext.Entry(parcel).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Parcel> GetAll()
        {
            return _dbContext.Parcels.AsEnumerable();
        }


        public Parcel GetById(int id)
        {
            return _dbContext.Parcels.Where(x => x.ID == id).FirstOrDefault();
        }

        public Parcel GetByTrackingId(string trackingID)
        {
            if (string.IsNullOrEmpty(trackingID))
            {
                throw new DALException("TrackingId is empty or has no value.");
            }

            return _dbContext.Parcels
                .Include(parcel => parcel.Receipient)
                .Include(parcel => parcel.Sender)
                .Include(parcel => parcel.VisitedHops)
                .Include(parcel => parcel.FutureHops)
                .SingleOrDefault(x => x.TrackingId == trackingID);
        }

        public void Update(Parcel p)
        {
            if(p == null)
            {
                throw new DALException("Parcel can not be null");
            }

            Parcel parcel = GetById(p.ID);
            if (parcel != null)
            {
                _dbContext.Entry(parcel).CurrentValues.SetValues(p);
                _dbContext.SaveChanges();
            }
        }
    }
}
