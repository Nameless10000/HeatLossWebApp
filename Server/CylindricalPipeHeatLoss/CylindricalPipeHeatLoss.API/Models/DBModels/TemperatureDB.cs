using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class TemperatureDB
    {
        [Key]
        public int ID { get; set; }

        [JsonIgnore]
        public ReportDB Report { get; set; }

        public int ReportID { get; set; }

        public double Value { get; set; }
    }
}
