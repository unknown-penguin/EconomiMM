using EconomiMM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomiMM.ViewModels
{
    public class CalcMaterialViewModel<T>
    {
        public T Material { get; set; }
        public SelectList MaterialTypes { get; set; }
    }
}
