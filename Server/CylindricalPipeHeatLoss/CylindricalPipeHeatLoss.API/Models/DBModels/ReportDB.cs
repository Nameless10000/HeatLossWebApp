using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    [PrimaryKey(nameof(ID), nameof(GeneratedAt))]
    public class ReportDB
    {
        public int ID { get; set; }

        public DateTime GeneratedAt { get; set; }

        public double TotalHeatLoss_Q { get; set; }

        public double LinearHeatFlowDensity_ql { get; set; }

        public double LinearHeatTransferCoefficient_kl { get; set; }

        public double PipeThermalResistance_Rl { get; set; }

        public List<TemperatureDB> Temperatures { get; set; }

        public List<DiameterDB> Diameters { get; set; }
        
        public List<ThermalResistanceDB> ThermalResistances { get; set; }

        public List<PipeLayerDB> PipeLayers { get; set; }

        public double PipeLength { get; set; }
    }
}
