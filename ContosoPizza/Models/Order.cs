using ContosoPizza.ModelBinders;
using ContosoPizza.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [ModelBinder(BinderType = typeof(CustomerNameModelBinder))]
        public CustomerName? CustomerName { get; set; }
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
