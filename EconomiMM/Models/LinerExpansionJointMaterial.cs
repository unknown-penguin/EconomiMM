using EconomiMM.Enum;
namespace EconomiMM.Models
{
    public class LinerExpansionJointMaterial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string normilized_name { get; set; }

        public ExpansionJointMaterialType Type { get; set; }

        public float Thickness { get; set; }

        public float Price { get; set; }
        public LinerParts PartOfLiner { get; set; } 

    }
}
