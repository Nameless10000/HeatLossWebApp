using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class ReportDB
    {
        [Key]
        public int ID { get; set; }

        public DateTime GeneratedAt { get; set; }

        public double Q { get; set; }

        public double InnerQl { get; set; }

        public double OutterQl { get; set; }

        public double a1 { get; set; }

        public double a2 { get; set; }

        public double e { get; set; }

        public double ql { get; set; }

        public double InnerTemp { get; set; }

        public double OutterTemp { get; set; }

        public List<TemperatureDB> Temperatures { get; set; }

        public List<RadiusDB> Radiuses { get; set; }

        public List<PipeLayerDB> PipeLayers { get; set; }

        public double PipeLength { get; set; }
    }
}
