using Geocoding;

namespace ParcelLogistics.SKS.Package.ServiceAgents.Interfaces
{
    public interface IGeoEncodingAgent
    {
        Location EncodeAddress(string address);
    }
}
