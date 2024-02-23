using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CylindricalPipeHeatLoss.Library.Models
{
    public class PipeLayer(double width, double thermalConductivityCoeff, string material = "Not set")
    {
        public double Width { get; set; } = width;

        public double ThermalConductivityCoeff { get; set; } = thermalConductivityCoeff;

        public string Material { get; set; } = material;
    }
}
