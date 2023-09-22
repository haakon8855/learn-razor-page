using ContosoPizza.Models;
using ContosoPizza.Models.ValueObjects;

namespace ContosoPizza.Utils
{
    public class OrderListSorter : ListSorter<Order>
    {
        public new enum SortCriteria
        {
            None,
            CustomerNameAscending,
            CustomerNameDescending,
            PizzaNameAscending,
            PizzaNameDescending,
            SodaNameAscending,
            SodaNameDescending,
            StatusAscending,
            StatusDescending
        }

        public SortCriteria SortBy { get; set; } = SortCriteria.None;
        public (int Sort, string Dir) CustomerNameSortRoute => SortBy switch
        {
            SortCriteria.CustomerNameDescending => ((int)SortCriteria.CustomerNameAscending, "up"),
            SortCriteria.CustomerNameAscending => ((int)SortCriteria.CustomerNameDescending, "down"),
            _ => ((int)SortCriteria.CustomerNameAscending, "")
        };
        public (int Sort, string Dir) PizzaNameSortRoute => SortBy switch
        {
            SortCriteria.PizzaNameDescending => ((int)SortCriteria.PizzaNameAscending, "up"),
            SortCriteria.PizzaNameAscending => ((int)SortCriteria.PizzaNameDescending, "down"),
            _ => ((int)SortCriteria.PizzaNameAscending, "")
        };
        public (int Sort, string Dir) SodaNameSortRoute => SortBy switch
        {
            SortCriteria.SodaNameDescending => ((int)SortCriteria.SodaNameAscending, "up"),
            SortCriteria.SodaNameAscending => ((int)SortCriteria.SodaNameDescending, "down"),
            _ => ((int)SortCriteria.SodaNameAscending, "")
        };
        public (int Sort, string Dir) StatusSortRoute => SortBy switch
        {
            SortCriteria.StatusDescending => ((int)SortCriteria.StatusAscending, "up"),
            SortCriteria.StatusAscending => ((int)SortCriteria.StatusDescending, "down"),
            _ => ((int)SortCriteria.StatusAscending, "")
        };

        public OrderListSorter(SortCriteria sortBy)
        {
            SortBy = sortBy;
        }

        public override IList<Order> Sort(IList<Order> orderList)
        {
            switch (SortBy)
            {
                case SortCriteria.CustomerNameAscending:
                    return orderList.OrderBy(o => o.CustomerName).ToList();
                case SortCriteria.CustomerNameDescending:
                    return orderList.OrderByDescending(o => o.CustomerName).ToList();
                case SortCriteria.PizzaNameAscending:
                    return orderList.OrderBy(o => o.Pizza != null ? o.Pizza.Name : new ProductName("___")).ToList();
                case SortCriteria.PizzaNameDescending:
                    return orderList.OrderByDescending(o => o.Pizza != null ? o.Pizza.Name : new ProductName("___")).ToList();
                case SortCriteria.SodaNameAscending:
                    return orderList.OrderBy(o => o.Soda != null ? o.Soda.Name : new ProductName("___")).ToList();
                case SortCriteria.SodaNameDescending:
                    return orderList.OrderByDescending(o => o.Soda != null ? o.Soda.Name : new ProductName("___")).ToList();
                case SortCriteria.StatusAscending:
                    return orderList.OrderBy(o => o.Status).ToList();
                case SortCriteria.StatusDescending:
                    return orderList.OrderByDescending(o => o.Status).ToList();
                default:
                    return orderList;
            }
        }
    }
}
