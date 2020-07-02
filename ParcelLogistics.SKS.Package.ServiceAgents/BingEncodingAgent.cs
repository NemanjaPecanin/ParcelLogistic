using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Geocoding;

using Newtonsoft.Json;
using ParcelLogistics.SKS.Package.ServiceAgents.Interfaces;

namespace ParcelLogistics.SKS.Package.ServiceAgents
{
    public class BingGeoEncodingAgent : IGeoEncodingAgent
    {
        static readonly string _key = "Ar1KRvCQcSqo8Kh9FeU5oUyfMlN-3R_U46LQhyB9CnhAl7hjdoNDWVJbzcdIbuCJ";

        public BingGeoEncodingAgent()
        {

        }

        public Location EncodeAddress(string address)
        {
            try
            {
                Uri geocodeRequest = new Uri(string.Format("http://dev.virtualearth.net/REST/v1/Locations?q={0}&key={1}", address, _key));
                var request = (HttpWebRequest)WebRequest.Create(geocodeRequest);
                var response = (HttpWebResponse)request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                string json = reader.ReadToEnd();
                dynamic deserializedObject = JsonConvert.DeserializeObject(json);
                double[] coordinates = new double[2];
                coordinates[0] = deserializedObject.resourceSets[0].resources[0].geocodePoints[0].coordinates[0];
                coordinates[1] = deserializedObject.resourceSets[0].resources[0].geocodePoints[0].coordinates[1];
                return new Location(coordinates[0], coordinates[1]);
            }
            catch (Exception exc)
            {
                throw new Exception("No search results found.", exc);
            }
        }
    }
}
