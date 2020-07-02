using ParcelLogistics.SKS.Package.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Interfaces
{
    public interface ITrackingPointRepository
    {
        void Create(Hop hop);
        void Delete(int id);
        Truck GetTruck(string code);
        Warehouse GetWarehouse(string code);
        Warehouse GetWarehouseByTruck(string TruckCode);
        IEnumerable<Truck> GetAllTrucks();
        IEnumerable<Hop> GetAllHops();
        
    }
}
