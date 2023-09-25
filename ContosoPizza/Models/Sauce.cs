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

        [Required]
        [ModelBinder(BinderType = typeof(CaloriesModelBinder))]
        public Calories? Calories { get; set; }
    }
}
