using ParcelLogistics.SKS.Package.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParcelLogistics.SKS.Package.BusinessLogic.Interfaces
{
    public interface IExportImportLogic
    {
        void ImportWarehouses(Warehouse warehouse);
        Warehouse ExportWarehouses();
    }
}
