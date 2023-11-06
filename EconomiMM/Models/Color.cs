using System.Drawing;

namespace EconomiMM.Models
{
    public class Color
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ColorHex { get; set; }

        public List<Material>? Materials { get; set; }

    }
}
