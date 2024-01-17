using AutoMapper;
using CityInfo.Entities;
using CityInfo.Models;

namespace CityInfo.MappingProfiles;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City, CityDto>();
        CreateMap<City, CityDtoWithoutPointOfInterest>();
    }
}
