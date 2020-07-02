using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;
using NetTopologySuite.Geometries;

namespace ParcelLogistics.SKS.Package.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DTOs.Parcel, BusinessLogic.Entities.Parcel>();
            CreateMap<DTOs.HopArrival, BusinessLogic.Entities.HopArrival>().ReverseMap();
            CreateMap<DTOs.Receipient, BusinessLogic.Entities.Receipient>().ReverseMap();
            CreateMap<DTOs.WarehouseNextHops, BusinessLogic.Entities.WarehouseNextHops>().ReverseMap();
            CreateMap<DTOs.TrackingInformation, BusinessLogic.Entities.Parcel>();
            CreateMap<DTOs.Hop, BusinessLogic.Entities.Hop>()
                .Include<DTOs.Warehouse, BusinessLogic.Entities.Warehouse>()
                .Include<DTOs.Truck, BusinessLogic.Entities.Truck>()
                .Include<DTOs.Transferwarehouse, BusinessLogic.Entities.Transferwarehouse>()
                .ReverseMap();
            CreateMap<DTOs.Warehouse, BusinessLogic.Entities.Warehouse>().ReverseMap();
            CreateMap<DTOs.Truck, BusinessLogic.Entities.Truck>().ReverseMap();
            CreateMap<DTOs.Transferwarehouse, BusinessLogic.Entities.Transferwarehouse>().ReverseMap();
            CreateMap<BusinessLogic.Entities.Parcel, DTOs.TrackingInformation>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.VisitedHops, opt => opt.MapFrom(src => src.VisitedHops))
                .ForMember(dest => dest.FutureHops, opt => opt.MapFrom(src => src.FutureHops))
                .ReverseMap()
                .ForAllOtherMembers(dest => dest.Ignore());



            CreateMap<BusinessLogic.Entities.Hop, DataAccess.Entities.Hop>()
                .Include<BusinessLogic.Entities.Warehouse, DataAccess.Entities.Warehouse>()
                .Include<BusinessLogic.Entities.Truck, DataAccess.Entities.Truck>()
                .Include<BusinessLogic.Entities.Transferwarehouse, DataAccess.Entities.Transferwarehouse>()
                .ReverseMap();




            CreateMap<string, Geometry>().ConvertUsing<GeoJsonToGeometryConverter>();
            CreateMap<Geometry, string>().ConvertUsing<GeometryToGeoJsonConverter>();

            CreateMap<BusinessLogic.Entities.HopArrival, DataAccess.Entities.HopArrival>().ReverseMap();

            CreateMap<BusinessLogic.Entities.Receipient, DataAccess.Entities.Receipient>().ReverseMap();

            CreateMap<BusinessLogic.Entities.Transferwarehouse, DataAccess.Entities.Transferwarehouse>().ForMember(dest => dest.RegionGeometry, opt => opt.MapFrom(src => src.RegionGeoJson)).ReverseMap();

            CreateMap<BusinessLogic.Entities.Truck, DataAccess.Entities.Truck>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.RegionGeometry, opt => opt.MapFrom(src => src.RegionGeoJson))
                .ReverseMap();

            CreateMap<BusinessLogic.Entities.Warehouse, DataAccess.Entities.Warehouse>().ReverseMap();
            CreateMap<BusinessLogic.Entities.WarehouseNextHops, DataAccess.Entities.WarehouseNextHops>().ReverseMap();





            CreateMap<BusinessLogic.Entities.Parcel, DataAccess.Entities.Parcel>()
                .ForMember(dest => dest.TrackingId, opt => opt.MapFrom(src => src.TrackingId.ToString())).ReverseMap();

            CreateMap<BusinessLogic.Entities.Receipient, DataAccess.Entities.Receipient>().ReverseMap();


      



        }
    }
}
