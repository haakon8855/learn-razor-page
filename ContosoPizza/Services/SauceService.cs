using ContosoPizza.Data;
using ContosoPizza.Models;

namespace ContosoPizza.Services
{
    public class SauceService
    {
        private readonly PizzaContext _context = default!;

        public SauceService(PizzaContext context)
        {
            _context = context;
        }

        public IList<Sauce> GetSauces()
        {
            if (_context.Sauces is not null)
            {
                return _context.Sauces.ToList();
            }
            return new List<Sauce>();
        }

        public Sauce? GetSauce(int id)
        {
            if (_context.Sauces is not null)
            {
                var sauce = _context.Sauces.Find(id);
                if (sauce is not null)
                {
                    return sauce;
                }
            }
            return null;
        }

        public void AddSauce(Sauce sauce)
        {
            if (_context.Sauces is not null)
            {
                _context.Sauces.Add(sauce);
                _context.SaveChanges();
            }
        }

        public void DeleteSauce(int id)
        {
            if (_context.Sauces is not null)
            {
                var sauce = _context.Sauces.Find(id);
                if (sauce is not null)
                {
                    _context.Sauces.Remove(sauce);
                    _context.SaveChanges();
                }
            }
        }
    }
}
