namespace ContosoPizza.Utils
{
    public abstract class ListSorter<T>
    {
        public enum SortCriteria { }
        public abstract IList<T> Sort(IList<T> list);
    }
}
