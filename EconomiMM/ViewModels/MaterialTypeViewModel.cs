using EconomiMM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomiMM.ViewModels
{
    public class MaterialTypeViewModel
    {
        public IEnumerable<MaterialType> MaterialTypes { get; set; }

        public List<string> Manufacturers { get; set; }
    }
}
