using System.ComponentModel.DataAnnotations;

namespace CityInfo.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(100)]
        public string UserName { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Family { get; set; }

        [Required, MaxLength(200)]
        public string Password { get; set; }
    }
}
