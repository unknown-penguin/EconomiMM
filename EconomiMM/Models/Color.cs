using System.Drawing;
using System.Text.Json.Serialization;
namespace EconomiMM.Models
{
    public class Color
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ColorHex { get; set; }
        [JsonIgnore]
        public List<Material>? Materials { get; set; } = new List<Material>();
        public List<ColorMaterial>? ColorMaterials { get; set; }
    }
}
