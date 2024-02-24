using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class TemperatureDB
    {
        [Key]
        public int ID { get; set; }

        public ReportDB Report { get; set; }

        public int ReportID { get; set; }

        public DateTime ReportGeneratedAt { get; set; }

        public double Value { get; set; }
    }
}
