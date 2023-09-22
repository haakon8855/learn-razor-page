using ContosoPizza.ModelBinders;
using ContosoPizza.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models
{
    public class Sauce
    {
        public int Id { get; set; }

        [Required]
        [ModelBinder(BinderType = typeof(ProductNameModelBinder))]
        public ProductName? Name { get; set; }
        public bool IsVegan { get; set; }

        [Range(0.01, 9999.99)]
        public decimal Calories { get; set; }
    }
}
