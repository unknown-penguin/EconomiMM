using System.ComponentModel.DataAnnotations.Schema;

namespace EconomiMM.Models
{
    public class ColorMaterial
    {

        [ForeignKey("MaterialId")]

        public int MaterialId { get; set; }
        public Material Material { get; set; }


        [ForeignKey("ColorId")]
        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
