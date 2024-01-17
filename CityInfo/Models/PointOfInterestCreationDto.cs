using System.ComponentModel.DataAnnotations;

namespace CityInfo.Models;

public class PointOfInterestCreationDto
{
    [Required(ErrorMessage = "This filed is required")]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(150)]
    public string? Description { get; set; }
}
