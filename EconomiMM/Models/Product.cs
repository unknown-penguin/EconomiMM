using EconomiMM.Enum;

namespace EconomiMM.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string orderNumber { get; set; }   

        public string temperature { get; set; }

        public JointShapes jointShape { get; set; }

        public JointType jointType { get; set; }

        public bool withLiner { get; set; }

        public float mainPartLength1 { get; set; }
        public float mainPartLength2 { get; set; }
        public float mainPartWidth { get; set; }

        public float flangeWidth { get; set; }
        public float koef { get; set; }

        public int siliconeTubes { get; set; }


        public float linerPartLength { get; set; }
        public float linerPartHeight { get; set; }
        public float linerPartWidth { get; set; }
        public float linerPartBindingWidth { get; set; }

        public float mainPartWorkPrice { get; set; }
        public float linerPartWorkPrice { get; set; }

        public List<SelectedMaterial<ExpansionJointMaterial>> ExpansionJointsMaterials { get; set; }
        public List<SelectedMaterial<FlangeMaterial>> flangeMaterials { get; set; }
        
        public List<SelectedMaterial<LinerExpansionJointMaterial>> LinerExpansionJointMaterials { get; set; }



    }
}
