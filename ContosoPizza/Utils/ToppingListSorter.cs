using ContosoPizza.Models;

namespace ContosoPizza.Utils
{
    public class ToppingListSorter : ListSorter<Topping>
    {
        public new enum SortCriteria
        {
            NameAscending,
            NameDescending,
            CaloriesAscending,
            CaloriesDescending
        }

        public SortCriteria SortBy { get; set; } = SortCriteria.NameAscending;
        public (int Sort, string Dir) ToppingNameSortRoute => SortBy switch
        {
            SortCriteria.NameDescending => ((int)SortCriteria.NameAscending, "up"),
            SortCriteria.NameAscending => ((int)SortCriteria.NameDescending, "down"),
            _ => ((int)SortCriteria.NameAscending, "")
        };
        public (int Sort, string Dir) ToppingCaloriesSortRoute => SortBy switch
        {
            SortCriteria.CaloriesDescending => ((int)SortCriteria.CaloriesAscending, "up"),
            SortCriteria.CaloriesAscending => ((int)SortCriteria.CaloriesDescending, "down"),
            _ => ((int)SortCriteria.CaloriesAscending, "")
        };

        public ToppingListSorter(SortCriteria sortBy)
        {
            SortBy = sortBy;
        }

        public override IList<Topping> Sort(IList<Topping> toppingList)
        {
            switch (SortBy)
            {
                case SortCriteria.NameAscending:
                    return toppingList.OrderBy(t => t.Name).ToList();
                case SortCriteria.NameDescending:
                    return toppingList.OrderByDescending(t => t.Name).ToList();
                case SortCriteria.CaloriesAscending:
                    return toppingList.OrderBy(t => t.Calories).ToList();
                case SortCriteria.CaloriesDescending:
                    return toppingList.OrderByDescending(t => t.Calories).ToList();
                default:
                    return toppingList;
            }
        }
    }
}
