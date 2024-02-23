using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CylindricalPipeHeatLoss.Library.Models
{
    public class ReportModel
    {
        public double Q { get; set; }

        public double Rl { get; set; }

        public double kl { get; set; }

        public double ql { get; set; }

        public List<double> Temperatures { get; set; }

        public List<double> Diameters { get; set; }

        public List<double> Rli { get; set; }

        public List<PipeLayer> PipeLayers { get; set; }

        public double PipeLength { get; set; }
    }
}
