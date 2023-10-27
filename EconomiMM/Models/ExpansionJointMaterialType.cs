using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EconomiMM.Models
{
    public class ExpansionJointMaterialType
    {
        [Display(Name = "Тип матеріалу")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
