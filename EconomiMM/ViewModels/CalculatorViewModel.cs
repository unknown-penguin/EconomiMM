using EconomiMM.Models;

namespace EconomiMM.ViewModels
{
    public class CalculatorViewModel
    {
        public IEnumerable<ExpansionJointMaterialType> jointMaterialTypes { get; set; }
        public IEnumerable<ExpansionJointMaterialType> flangeMaterialTypes { get; set; }
        public IEnumerable<ExpansionJointMaterialType> innerLinerMaterialTypes { get; set; }
        public IEnumerable<ExpansionJointMaterialType> outerLinerMaterialTypes { get; set; }

        public IEnumerable<ExpansionJointMaterial> expansionJointMaterials { get; set; }
        public IEnumerable<FlangeMaterial> flangeMaterials { get; set; }
        public IEnumerable<LinerExpansionJointMaterial> linerExpansionJointMaterials { get; set; }
    }
}
