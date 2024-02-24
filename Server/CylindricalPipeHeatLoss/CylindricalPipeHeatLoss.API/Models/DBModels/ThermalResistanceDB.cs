using DocumentFormat.OpenXml.Presentation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class ThermalResistanceDB
    {
        [Key]
        public int ID { get; set; }

        public ReportDB Report { get; set; }

        public int ReportID { get; set; }

        public DateTime ReportGeneratedAt { get; set; }

        public double Value { get; set; }
    }
}
