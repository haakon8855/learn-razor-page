using ContosoPizza.ModelBinders;
using ContosoPizza.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models
{
    public class Topping
    {
        public int Id { get; set; }

        [Required]
        [ModelBinder(BinderType = typeof(ProductNameModelBinder))]
        public ProductName? Name { get; set; }

        [Range(0.0, 9999.99)]
        public decimal? Calories { get; set; }
        public ICollection<Pizza>? Pizzas { get; set; }
    }
}
