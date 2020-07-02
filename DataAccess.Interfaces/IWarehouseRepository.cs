using ParcelLogistics.SKS.Package.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.DataAccess.Interfaces
{
    public interface IWarehouseRepository
    {
        int Create(Hop hop);
        void Update(Hop hop);
        void Delete(int id);

        void Clear();

        Warehouse GetRootWarehouse();
        Hop GetByCoordinates(double longitude, double latitude);
        IEnumerable<Warehouse> GetAllWarehouses();
        Hop GetByCode(string code);

    }
}
