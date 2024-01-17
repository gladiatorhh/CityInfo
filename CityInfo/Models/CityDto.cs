namespace CityInfo.Models;

public class CityDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = 
        new List<PointOfInterestDto>();
}
