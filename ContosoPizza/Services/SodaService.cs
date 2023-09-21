using ContosoPizza.Data;
using ContosoPizza.Models;

namespace ContosoPizza.Services
{
    public class SodaService
    {
        private readonly PizzaContext _context = default!;

        public SodaService(PizzaContext context)
        {
            _context = context;
        }

        public IList<Soda> GetSodas()
        {
            if (_context.Sodas is not null)
            {
                return _context.Sodas.ToList();
            }
            return new List<Soda>();
        }

        public Soda? GetSoda(int id)
        {
            if (_context.Sodas is not null)
            {
                var soda = _context.Sodas.Find(id);
                if (soda is not null)
                {
                    return soda;
                }
            }
            return null;
        }

        public void AddSoda(Soda soda)
        {
            if (_context.Sodas is not null)
            {
                _context.Sodas.Add(soda);
                _context.SaveChanges();
            }
        }

        public void DeleteSoda(int id)
        {
            if (_context.Sodas is not null)
            {
                var soda = _context.Sodas.Find(id);
                if (soda is not null)
                {
                    _context.Sodas.Remove(soda);
                    _context.SaveChanges();
                }
            }
        }
    }
}
