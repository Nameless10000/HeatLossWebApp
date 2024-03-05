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

        public double InnerQl { get; set; }

        public double OutterQl { get; set; }

        public double a2 { get; set; }

        public double a1 { get; set; }

        public double ql { get; set; }

        public double e { get; set; }

        public double OutterTemp { get; set; }

        public double InnerTemp { get; set; }

        public List<double> Temperatures { get; set; }

        public List<double> Radiuses { get; set; }

        public List<PipeLayer> PipeLayers { get; set; }

        public double PipeLength { get; set; }
    }
}
