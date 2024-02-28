using CylindricalPipeHeatLoss.Library.Models;

namespace CylindricalPipeHeatLoss.API.Models
{
    public class HeatLossRequestDTO
    {
        public double InnerPipeRadius { get; set; }
            
        public double PipeLength { get; set; }

        public List<PipeLayer> PipeLayers { get; set; }
        
        public double A1 { get; set; }

        public double E { get; set; }
        
        public double InnerTemp { get; set; }

        public double OutterTemp { get; set; }

        public double Precision { get; set; }

        public PipeOrientation PipeOrientation { get; set; }

    }
}
