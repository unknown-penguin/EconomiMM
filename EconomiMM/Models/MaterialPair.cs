namespace EconomiMM.Models
{
    public class SelectedMaterial<T>
    {
        public int Id { get; set; }  
        public T Material { get; set; }
        public int Count { get; set; }
        public SelectedMaterial()
        {
        }
        public SelectedMaterial(T material, int count)
        {
            Material = material;
            Count = count;
        }
    }
    public class MaterialCountTuple
    {
        public int id { get; set; }
        public int countOfLayers { get; set; }

    }
}
