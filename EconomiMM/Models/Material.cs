using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace EconomiMM.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Size { get; set; }
        public List<Color>? Colors { get; set; }= new List<Color>();

        public List<ColorMaterial>? ColorMaterials { get; set; }
        public float Thickness { get; set; }

        public int Count { get; set; } 

        public int Reserved { get; set; }

        public int Price { get; set; }

        
        public int OurPriceForSheet { get; set; }

        public int OurPriceForSqMetre { get; set; }

        
        public int DealerPriceForSheet { get; set; }
        public int DealerPriceForSqMetre { get; set; }



        public int Sold { get; set; }


    }
}
