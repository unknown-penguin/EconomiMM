namespace EconomiMM.Models
{
    public class SellHistory
    {
        public int Id {  get; set; }

        public Material material { get; set; }

        public int Count { get; set; }

        public int Sold { get; set; }

    }
}
