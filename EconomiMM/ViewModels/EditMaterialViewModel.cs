using EconomiMM.Models;

namespace EconomiMM.ViewModels
{
    public class EditMaterialViewModel
    {
        public Material Material {get;set;}

        public List<Color>? Colors { get; set; }

        public string? SelectedColorsId { get;set;}

        public PriceKoeficients PriceKoeficients { get; set; }

    }
}
