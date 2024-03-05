using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CylindricalPipeHeatLoss.Library.Models
{
    public class PipeLayer(Material material, double width)
    {
        public double Width { get; set; } = width;

        public double ThermalConductivityCoeff { get; set; }

        public double Ql { get; set; }

        public Material Material { get; set; } = material;

        public PipeLayer() : this(new(0, 0, 0, ""), 0) { }

        public double GetThermalConductivityCoeff(double temp) => Material.ACoeff * temp * temp + Material.BCoeff * temp + Material.CCoeff;
    }
}
