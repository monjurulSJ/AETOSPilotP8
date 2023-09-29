using AutoMapper;
using P8.Model.DTO;
using P8.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8.Model.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            //Source to Destination
            
            CreateMap<Reading, Temperature>().ForMember(dest => dest.Id, src => src.Ignore());
            CreateMap<Geo, DeviceInfo>().ForMember(dest => dest.Latitude, src => src.MapFrom(src => src.Lat))
                                        .ForMember(des=>des.Longitude,src=>src.MapFrom(src=>src.Long));
            CreateMap<VehicleDTO, Vehicle>().ForMember(dest => dest.Id, src => src.Ignore());




        }
    }
}
