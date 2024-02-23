using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CylindricalPipeHeatLoss.Library.Models
{
    public class ReportModel
    {
        public double Q { get; internal set; }

        public double Rl { get; internal set; }

        public double kl { get; internal set; }

        public double ql { get; internal set; }

        public List<double> Temperatures { get; internal set; }

        public List<double> Diameters { get; internal set; }

        public List<double> Rli { get; internal set; }

        public List<PipeLayer> PipeLayers { get; internal set; }

        public double PipeLength { get; internal set; }
    }
}
