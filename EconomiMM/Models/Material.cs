using System.ComponentModel.DataAnnotations;

namespace EconomiMM.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Size { get; set; }

        public float Thickness { get; set; }

        public int Count { get; set; } 

        public int Reserved { get; set; }

        public int Price { get; set; }

        public int Sold { get; set; }


    }
}
