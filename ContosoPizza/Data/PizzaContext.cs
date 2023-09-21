using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Data
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options)
            : base(options)
        {
        }

        public DbSet<ContosoPizza.Models.Pizza>? Pizzas { get; set; }
        public DbSet<ContosoPizza.Models.Sauce>? Sauces { get; set; }
        public DbSet<ContosoPizza.Models.Topping>? Toppings { get; set; }
        public DbSet<ContosoPizza.Models.Soda>? Sodas { get; set; }
        public DbSet<ContosoPizza.Models.Order>? Orders { get; set; }
    }
}
