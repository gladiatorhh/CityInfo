using AutoMapper;
using CityInfo.Entities;
using CityInfo.Models;

namespace CityInfo.MappingProfiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<PointOfInterest,PointOfInterestDto>();
            CreateMap<PointOfInterest,PointOfInterestEditDto>();
            CreateMap<PointOfInterestCreationDto, PointOfInterest>();
            CreateMap<PointOfInterestEditDto,PointOfInterest>();
        }
    }
}
