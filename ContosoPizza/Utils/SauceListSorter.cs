using ContosoPizza.Models;

namespace ContosoPizza.Utils
{
    public class SauceListSorter : ListSorter<Sauce>
    {
        public new enum SortCriteria
        {
            NameAscending,
            NameDescending,
            VeganAscending,
            VeganDescending,
            CaloriesAscending,
            CaloriesDescending
        }

        public SortCriteria SortBy { get; set; } = SortCriteria.NameAscending;
        public (int Sort, string Dir) SauceNameSortRoute => SortBy switch
        {
            SortCriteria.NameDescending => ((int)SortCriteria.NameAscending, "up"),
            SortCriteria.NameAscending => ((int)SortCriteria.NameDescending, "down"),
            _ => ((int)SortCriteria.NameAscending, "")
        };
        public (int Sort, string Dir) SauceVeganSortRoute => SortBy switch
        {
            SortCriteria.VeganDescending => ((int)SortCriteria.VeganAscending, "up"),
            SortCriteria.VeganAscending => ((int)SortCriteria.VeganDescending, "down"),
            _ => ((int)SortCriteria.VeganAscending, "")
        };
        public (int Sort, string Dir) SauceCaloriesSortRoute => SortBy switch
        {
            SortCriteria.CaloriesDescending => ((int)SortCriteria.CaloriesAscending, "up"),
            SortCriteria.CaloriesAscending => ((int)SortCriteria.CaloriesDescending, "down"),
            _ => ((int)SortCriteria.CaloriesAscending, "")
        };

        public SauceListSorter(SortCriteria sortBy)
        {
            SortBy = sortBy;
        }

        public override IList<Sauce> Sort(IList<Sauce> sauceList)
        {
            switch (SortBy)
            {
                case SortCriteria.NameAscending:
                    return sauceList.OrderBy(s => s.Name).ToList();
                case SortCriteria.NameDescending:
                    return sauceList.OrderByDescending(s => s.Name).ToList();
                case SortCriteria.VeganAscending:
                    return sauceList.OrderBy(s => s.IsVegan).ToList();
                case SortCriteria.VeganDescending:
                    return sauceList.OrderByDescending(s => s.IsVegan).ToList();
                case SortCriteria.CaloriesAscending:
                    return sauceList.OrderBy(s => s.Calories).ToList();
                case SortCriteria.CaloriesDescending:
                    return sauceList.OrderByDescending(s => s.Calories).ToList();
                default:
                    return sauceList;
            }
        }

    }
}
