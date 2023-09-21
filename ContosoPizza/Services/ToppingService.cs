using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services
{
    public class ToppingService
    {
        private readonly PizzaContext _context = default!;

        public ToppingService(PizzaContext context)
        {
            _context = context;
        }

        public IList<Topping> GetToppings()
        {
            if (_context.Toppings is not null)
            {
                return _context.Toppings.ToList();
            }
            return new List<Topping>();
        }

        public Topping? GetTopping(int id)
        {
            if (_context.Toppings is not null)
            {
                var topping = _context.Toppings.Find(id);
                if (topping is not null)
                {
                    return topping;
                }
            }
            return null;
        }

        public void AddTopping(Topping topping)
        {
            if (_context.Toppings is not null)
            {
                _context.Toppings.Add(topping);
                _context.SaveChanges();
            }
        }

        public void DeleteTopping(int id)
        {
            if (_context.Toppings is not null)
            {
                var topping = _context.Toppings.Find(id);
                if (topping is not null)
                {
                    _context.Toppings.Remove(topping);
                    _context.SaveChanges();
                }
            }
        }

        public void AddPizza(int id, Pizza pizza)
        {
            if (_context.Toppings is not null)
            {
                var topping = _context.Toppings
                    .Include(t => t.Pizzas)
                    .ToList()
                    .Find(t => t.Id == id);
                if (topping is not null)
                {
                    if (topping.Pizzas == null)
                    {
                        topping.Pizzas = new List<Pizza>();
                    }
                    topping.Pizzas.Add(pizza);
                    _context.SaveChanges();
                }
            }
        }

        public void DeletePizza(int id, Pizza pizza)
        {
            if (_context.Toppings is not null)
            {
                var topping = _context.Toppings
                    .Include(t => t.Pizzas)
                    .ToList()
                    .Find(t => t.Id == id);
                if (topping is not null)
                {
                    if (topping.Pizzas == null)
                    {
                        topping.Pizzas = new List<Pizza>();
                    }
                    else
                    {
                        topping.Pizzas.Remove(pizza);
                    }
                    _context.SaveChanges();
                }
            }
        }
    }
}
