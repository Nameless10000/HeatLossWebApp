using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class TemperatureDB
    {
        [Key]
        [XmlIgnore]
        public int ID { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public ReportDB Report { get; set; }

        [XmlIgnore]
        public int ReportID { get; set; }

        [XmlElement(ElementName = "Temperatures between layers, °C")]
        public double Value { get; set; }
    }
}
