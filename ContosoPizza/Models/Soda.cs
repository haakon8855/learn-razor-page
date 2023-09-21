using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models
{

    public class Soda
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public bool IsSugarFree { get; set; }

        [Range(0.0, 9999.99)]
        public decimal Calories { get; set; }
    }
}

