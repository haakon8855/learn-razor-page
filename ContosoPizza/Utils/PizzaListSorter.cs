using ContosoPizza.Models;
using ContosoPizza.Models.ValueObjects;

namespace ContosoPizza.Utils
{
    public class PizzaListSorter : ListSorter<Pizza>
    {
        public new enum SortCriteria
        {
            None,
            NameAscending,
            NameDescending,
            PriceAscending,
            PriceDescending,
            SizeAscending,
            SizeDescending,
            SauceAscending,
            SauceDescending,
            GlutenFreeAscending,
            GlutenFreeDescending
        }

        public SortCriteria SortBy { get; set; } = SortCriteria.None;
        public (int Sort, string Dir) PizzaNameSortRoute => SortBy switch
        {
            SortCriteria.NameDescending => ((int)SortCriteria.NameAscending, "up"),
            SortCriteria.NameAscending => ((int)SortCriteria.NameDescending, "down"),
            _ => ((int)SortCriteria.NameAscending, "")
        };
        public (int Sort, string Dir) PizzaPriceSortRoute => SortBy switch
        {
            SortCriteria.PriceDescending => ((int)SortCriteria.PriceAscending, "up"),
            SortCriteria.PriceAscending => ((int)SortCriteria.PriceDescending, "down"),
            _ => ((int)SortCriteria.PriceAscending, "")
        };
        public (int Sort, string Dir) PizzaSizeSortRoute => SortBy switch
        {
            SortCriteria.SizeDescending => ((int)SortCriteria.SizeAscending, "up"),
            SortCriteria.SizeAscending => ((int)SortCriteria.SizeDescending, "down"),
            _ => ((int)SortCriteria.SizeAscending, "")
        };
        public (int Sort, string Dir) PizzaSauceSortRoute => SortBy switch
        {
            SortCriteria.SauceDescending => ((int)SortCriteria.SauceAscending, "up"),
            SortCriteria.SauceAscending => (((int)SortCriteria.SauceDescending, "down")),
            _ => ((int)SortCriteria.SauceAscending, "")
        };
        public (int Sort, string Dir) PizzaGlutenFreeSortRoute => SortBy switch
        {
            SortCriteria.GlutenFreeDescending => ((int)SortCriteria.GlutenFreeAscending, "up"),
            SortCriteria.GlutenFreeAscending => ((int)SortCriteria.GlutenFreeDescending, "down"),
            _ => ((int)SortCriteria.GlutenFreeAscending, "")
        };

        public PizzaListSorter(SortCriteria sortBy)
        {
            SortBy = sortBy;
        }

        public override IList<Pizza> Sort(IList<Pizza> pizzaList)
        {
            switch (SortBy)
            {
                case SortCriteria.NameAscending:
                    return pizzaList.OrderBy(p => p.Name).ToList();
                case SortCriteria.NameDescending:
                    return pizzaList.OrderByDescending(p => p.Name).ToList();
                case SortCriteria.PriceAscending:
                    return pizzaList.OrderBy(p => p.Price).ToList();
                case SortCriteria.PriceDescending:
                    return pizzaList.OrderByDescending(p => p.Price).ToList();
                case SortCriteria.SizeAscending:
                    return pizzaList.OrderBy(p => p.Size).ToList();
                case SortCriteria.SizeDescending:
                    return pizzaList.OrderByDescending(p => p.Size).ToList();
                case SortCriteria.SauceAscending:
                    return pizzaList.OrderBy(p => p.Sauce != null ? p.Sauce.Name : new ProductName("___")).ToList();
                case SortCriteria.SauceDescending:
                    return pizzaList.OrderByDescending(p => p.Sauce != null ? p.Sauce.Name : new ProductName("___")).ToList();
                case SortCriteria.GlutenFreeAscending:
                    return pizzaList.OrderBy(p => p.IsGlutenFree).ToList();
                case SortCriteria.GlutenFreeDescending:
                    return pizzaList.OrderByDescending(p => p.IsGlutenFree).ToList();
                default:
                    return pizzaList;
            }
        }
    }
}