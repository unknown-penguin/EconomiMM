namespace EconomiMM.Models
{
    public class SelectedMaterial<T>
        : Tuple<T, int>
    {
        public T Material;
        public int Count;
        public SelectedMaterial(T item1, int item2) : base(item1, item2)
        {
            Material = item1;
            Count = item2;
        }
    }
}
