using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.Entities;

public class PointOfInterest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    public string Description { get; set; }

    public int CityId { get; set; }

    [ForeignKey(nameof(CityId))]
    public City City { get; set; }
}
