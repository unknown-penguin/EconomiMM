using EconomiMM.Enum;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EconomiMM.Models
{
    public class Product
    {
        public int? Id { get; set; }

        [Display(Name = "Номер замовлення")]
        public string orderNumber { get; set; }

        [Display(Name = "Температура")]
        public string temperature { get; set; }

        [Display(Name = "Форма компенсатора")]
        public JointShapes jointShape { get; set; }

        [Display(Name = "Тип компенсатора")]
        public JointType jointType { get; set; }

        [Display(Name = "З вкладишем")]
        public bool withLiner { get; set; }

        [Display(Name = "Довжина головної частини 1")]
        public float? mainPartLength1 { get; set; }

        [Display(Name = "Діаметр головної частини")]
        public float? mainPartDiameter { get; set; }

        [Display(Name = "Довжина головної частини 2")]
        public float mainPartLength2 { get; set; }

        [Display(Name = "Ширина головної частини")]
        public float mainPartWidth { get; set; }

        [Display(Name = "Ширина фланця")]
        public float flangeWidth { get; set; }

        [Display(Name = "Коефіцієнт")]
        public float koef { get; set; }

        [Display(Name = "Силікон")]
        public int siliconeTubes { get; set; }

        [Display(Name = "Довжина вкладиша")]
        public float linerPartLength { get; set; }

        [Display(Name = "Висота вкладиша")]
        public float linerPartHeight { get; set; }

        [Display(Name = "Ширина вкладиша")]
        public float linerPartWidth { get; set; }

        [Display(Name = "Ширина кріплення вкладиша")]
        public float linerPartBindingWidth { get; set; }

        [Display(Name = "Вартість роботи головної частини")]
        public float mainPartWorkPrice { get; set; }

        [Display(Name = "Вартість роботи вкладиша")]
        public float linerPartWorkPrice { get; set; }

        
        public List<SelectedMaterial<ExpansionJointMaterial>>? ExpansionJointsMaterials { get; set; } = new List<SelectedMaterial<ExpansionJointMaterial>>();

        
        public List<SelectedMaterial<FlangeMaterial>>? flangeMaterials { get; set; } = new List<SelectedMaterial<FlangeMaterial>>();

        public List<SelectedMaterial<LinerExpansionJointMaterial>>? LinerExpansionJointMaterials { get; set; } = new List<SelectedMaterial<LinerExpansionJointMaterial>>();

        [Display(Name = "Загальна вартість")]
        public float TotalPrice { get; set; } = 0;


    }
    
}
