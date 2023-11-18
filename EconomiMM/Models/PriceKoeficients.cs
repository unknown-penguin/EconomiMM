using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EconomiMM.Models
{
    public class PriceKoeficients
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        [Display(Name = "Наша ціна за лист")]
        public float OurPriceForSheetKoef { get; set; }
        [Display(Name = "Наша ціна за метр^2")]
        public float OurPriceForSqMetreKoef { get; set; }
        [Display(Name = "Дилерська ціна за лист")]
        public float DealerPriceForSheetKoef { get; set; }
        [Display(Name = "Дилерська ціна за метр^2")]
        public float DealerPriceForSqMetreKoef { get; set; }
    }
}
