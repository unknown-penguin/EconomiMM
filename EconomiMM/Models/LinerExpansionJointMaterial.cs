using EconomiMM.Enum;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EconomiMM.Models
{
    public class LinerExpansionJointMaterial
    {
        public int Id { get; set; }
        [Display(Name = "Назва")]
        public string Name { get; set; }

        public ExpansionJointMaterialType Type { get; set; }
        [Display(Name = "Товщина")]
        public float Thickness { get; set; }
        [Display(Name = "Ціна")]
        public float Price { get; set; }
        [Display(Name = "Частина вкладиша")]
        public LinerParts PartOfLiner { get; set; } 

    }
}
