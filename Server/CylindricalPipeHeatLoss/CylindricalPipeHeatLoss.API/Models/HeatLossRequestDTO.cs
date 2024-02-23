using CylindricalPipeHeatLoss.Library.Models;

namespace CylindricalPipeHeatLoss.API.Models
{
    public class HeatLossRequestDTO
    {
        public double InnerPipeDiameter { get; set; }
            
        public double PipeLength { get; set; }

        public List<PipeLayer> PipeLayers { get; set; }
        
        public double A1 { get; set; }

        public double A2 { get; set; }
        
        public List<double> Temps { get; set; }
    }
}
