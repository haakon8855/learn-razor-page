using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string? CustomerName { get; set; }
        public Pizza? Pizza { get; set; }
        public Soda? Soda { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        New,
        Preparing,
        Delivering,
        Completed
    };
}
