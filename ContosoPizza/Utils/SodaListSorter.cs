using ContosoPizza.Models;

namespace ContosoPizza.Utils
{
    public class SodaListSorter : ListSorter<Soda>
    {
        public new enum SortCriteria
        {
            NameAscending,
            NameDescending,
            CaloriesAscending,
            CaloriesDescending,
            SugarFreeAscending,
            SugarFreeDescending
        }
        public SortCriteria SortBy { get; set; } = SortCriteria.NameAscending;
        public (int Sort, string Dir) SodaNameSortRoute => SortBy switch
        {
            SortCriteria.NameDescending => ((int)SortCriteria.NameAscending, "up"),
            SortCriteria.NameAscending => ((int)SortCriteria.NameDescending, "down"),
            _ => ((int)SortCriteria.NameAscending, "")
        };
        public (int Sort, string Dir) SodaCaloriesSortRoute => SortBy switch
        {
            SortCriteria.CaloriesDescending => ((int)SortCriteria.CaloriesAscending, "up"),
            SortCriteria.CaloriesAscending => ((int)SortCriteria.CaloriesDescending, "down"),
            _ => ((int)SortCriteria.CaloriesAscending, "")
        };
        public (int Sort, string Dir) SodaSugarFreeSortRoute => SortBy switch
        {
            SortCriteria.SugarFreeDescending => ((int)SortCriteria.SugarFreeAscending, "up"),
            SortCriteria.SugarFreeAscending => ((int)SortCriteria.SugarFreeDescending, "down"),
            _ => ((int)SortCriteria.SugarFreeAscending, "")
        };
        public SodaListSorter(SortCriteria sortBy)
        {
            SortBy = sortBy;
        }

        public override IList<Soda> Sort(IList<Soda> sodaList)
        {
            switch (SortBy)
            {
                case SortCriteria.NameAscending:
                    return sodaList.OrderBy(s => s.Name).ToList();
                case SortCriteria.NameDescending:
                    return sodaList.OrderByDescending(s => s.Name).ToList();
                case SortCriteria.CaloriesAscending:
                    return sodaList.OrderBy(s => s.Calories).ToList();
                case SortCriteria.CaloriesDescending:
                    return sodaList.OrderByDescending(s => s.Calories).ToList();
                case SortCriteria.SugarFreeAscending:
                    return sodaList.OrderBy(s => s.IsSugarFree).ToList();
                case SortCriteria.SugarFreeDescending:
                    return sodaList.OrderByDescending(s => s.IsSugarFree).ToList();
                default:
                    return sodaList;
            }
        }
    }
}
