using ParcelLogistics.SKS.Package.Services.DTOs;
using System;
using Newtonsoft.Json.Linq;

namespace ParcelLogistics.SKS.Package.Services.Helpers
{
    public class HopJsonConverter : JsonCreationConverter<Hop>
    {
        protected override Hop Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["nextHops"] != null)
                return new Warehouse();
            else if (jObject["numberPlate"] != null)
                return new Truck();
            else if (jObject["logisticsPartnerUrl"] != null)
                return new Transferwarehouse();
            else
                throw new Newtonsoft.Json.JsonSerializationException("Not a valid subclass of abstract class Hop!");
        }
    }
}
