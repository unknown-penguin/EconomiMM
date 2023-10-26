namespace EconomiMM.Models
{
    public class ExpansionJointMaterial
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ExpansionJointMaterialType Type { get; set; }

        public float Thickness { get; set; }

        public float Price { get; set; }

    }
}
