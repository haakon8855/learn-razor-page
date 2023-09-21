using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services
{
    public class OrderService
    {
        private readonly PizzaContext _context = default!;

        public OrderService(PizzaContext context)
        {
            _context = context;
        }

        public IList<Order> GetOrders()
        {
            if (_context.Orders is not null)
            {
                return _context.Orders
                    .Include(o => o.Pizza)
                    .Include(o => o.Soda)
                    .ToList();
            }
            return new List<Order>();
        }

        public Order? GetOrder(int id)
        {
            if (_context.Orders is not null)
            {
                var order = _context.Orders.Find(id);
                if (order != null)
                {
                    return order;
                }
            }
            return null;
        }

        public void AddOrder(Order order)
        {
            if (_context.Orders is not null)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
        }

        public void DeleteOrder(int id)
        {
            if (_context.Orders is not null)
            {
                var order = _context.Orders.Find(id);
                if (order is not null)
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                }
            }
        }

        public void AdvanceOrderStatus(int id)
        {
            if (_context.Orders is not null)
            {
                var order = _context.Orders.Find(id);
                if (order != null && order.Status < OrderStatus.Completed)
                {
                    order.Status++;
                }
                _context.SaveChanges();
            }
        }

        public void RevertOrderStatus(int id)
        {
            if (_context.Orders is not null)
            {
                var order = _context.Orders.Find(id);
                if (order != null && order.Status > OrderStatus.New)
                {
                    order.Status--;
                }
                _context.SaveChanges();
            }
        }
    }
}
