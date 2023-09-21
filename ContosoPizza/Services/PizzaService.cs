using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services
{
    public class PizzaService
    {
        private readonly PizzaContext _context = default!;

        public PizzaService(PizzaContext context)
        {
            _context = context;
        }

        public IList<Pizza> GetPizzas()
        {
            if (_context.Pizzas is not null)
            {
                return _context.Pizzas
                    .Include(c => c.Sauce)
                    .Include(c => c.Toppings)
                    .ToList();
            }
            return new List<Pizza>();
        }
        public Pizza? GetPizza(int id)
        {
            if (_context.Pizzas is not null)
            {
                var pizza = _context.Pizzas
                    .Include(c => c.Sauce)
                    .Include(c => c.Toppings)
                    .ToList()
                    .Find(p => p.Id == id);
                if (pizza is not null)
                {
                    return pizza;
                }
            }
            return null;
        }

        public void AddPizza(Pizza pizza)
        {
            if (_context.Pizzas is not null)
            {
                _context.Pizzas.Add(pizza);
                _context.SaveChanges();
            }
        }

        public void DeletePizza(int id)
        {
            if (_context.Pizzas is not null)
            {
                var pizza = _context.Pizzas.Find(id);
                if (pizza is not null)
                {
                    _context.Pizzas.Remove(pizza);
                    _context.SaveChanges();
                }
            }
        }

        public void AddSauce(int id, Sauce sauce)
        {
            if (_context.Pizzas is not null)
            {
                var pizza = _context.Pizzas.Find(id);
                if (pizza is not null)
                {
                    pizza.Sauce = sauce;
                    _context.SaveChanges();
                }
            }
        }

        public void AddTopping(int id, Topping topping)
        {
            if (_context.Pizzas != null)
            {
                var pizza = _context.Pizzas
                    .Include(p => p.Toppings)
                    .ToList()
                    .Find(p => p.Id == id);
                if (pizza is not null)
                {
                    if (pizza.Toppings == null)
                    {
                        pizza.Toppings = new List<Topping>();
                    }
                    pizza.Toppings.Add(topping);
                    _context.SaveChanges();
                }
            }
        }

        public void DeleteTopping(int id, Topping topping)
        {
            if (_context.Pizzas != null)
            {
                var pizza = _context.Pizzas
                    .Include(p => p.Toppings)
                    .ToList()
                    .Find(p => p.Id == id);
                if (pizza is not null)
                {
                    if (pizza.Toppings == null)
                    {
                        pizza.Toppings = new List<Topping>();
                    }
                    else
                    {
                        pizza.Toppings.Remove(topping);
                    }
                    _context.SaveChanges();
                }
            }
        }
    }
}
