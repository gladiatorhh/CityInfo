using CityInfo.Models;

namespace CityInfo;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }

    public CitiesDataStore()
    {
        Cities = new List<CityDto>
        {
            new CityDto{
                Id = 1,
                Name = "Tehran",
                Description = "Iran",
                PointsOfInterest = new List<PointOfInterestDto>
                {
                    new PointOfInterestDto{Id = 1,Name = "Point of interenst number 1",Description = "Point of interenst number 1 des."},
                    new PointOfInterestDto{Id = 2,Name = "Point of interenst number 2",Description = "Point of interenst number 2 des."},
                }
            },
            new CityDto
            {
                Id = 2,
                Name = "Los Angles",
                Description = "USA",
                PointsOfInterest = new List<PointOfInterestDto>
                {
                    new PointOfInterestDto{Id = 3,Name = "Point of interenst number 3",Description = "Point of interenst number 3 des."},
                    new PointOfInterestDto{Id = 4,Name = "Point of interenst number 4",Description = "Point of interenst number 4 des."},
                }
            },
            new CityDto
            {
                Id = 3,
                Name = "San Fernando",
                Description = "USA",
                PointsOfInterest = new List<PointOfInterestDto>
                {
                    new PointOfInterestDto{Id = 5,Name = "Point of interenst number 5",Description = "Point of interenst number 5 des."},
                    new PointOfInterestDto{Id = 6,Name = "Point of interenst number 6",Description = "Point of interenst number 7 des."},
                }
            },
            new CityDto{
                Id = 4,
                Name = "Yazd",
                Description = "Iran",
                PointsOfInterest = new List<PointOfInterestDto>
                {
                    new PointOfInterestDto{Id = 7,Name = "Point of interenst number 3",Description = "Point of interenst number 7 des."},
                    new PointOfInterestDto{Id = 8,Name = "Point of interenst number 8",Description = "Point of interenst number 8 des."},
                }
            },
            new CityDto
            {
                Id = 5,
                Name = "Boston",
                Description = "USA"
            },
        };
    }
}
