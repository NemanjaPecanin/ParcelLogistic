using System;
using AutoMapper;
using Newtonsoft.Json;
using NetTopologySuite.Geometries;
using NetTopologySuite.Features;
using System.IO;
using System.Text;
using NetTopologySuite.IO;

namespace ParcelLogistics.SKS.Package.Services.Mapper
{
    public class GeoJsonToGeometryConverter : ITypeConverter<string, Geometry>
    {
        public Geometry Convert(string source, Geometry destination, ResolutionContext context)
        {
            if (source == null)
                return null;

            var serializer = GeoJsonSerializer.CreateDefault();
            return ((Feature)serializer.Deserialize(new StringReader(source), typeof(Feature))).Geometry;
        }
    }

    public class GeometryToGeoJsonConverter : ITypeConverter<Geometry, string>
    {
        public string Convert(Geometry source, string destination, ResolutionContext context)
        {
            if (source == null)
                return null;

            var serializer = GeoJsonSerializer.CreateDefault();
            var writer = new StringWriter();
            serializer.Serialize(writer, source, typeof(Feature));
            return writer.ToString();
        }
    }
}