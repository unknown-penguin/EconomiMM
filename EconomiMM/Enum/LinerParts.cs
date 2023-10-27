using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EconomiMM.Enum
{
    public enum LinerParts
    {
        [Display(Name = "Внутрішня частина")]
        Inner,
        [Display(Name = "Звонішня частина")]
        Outer
    }
}
