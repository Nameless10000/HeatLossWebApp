using DocumentFormat.OpenXml.Presentation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class RadiusDB
    {
        [Key]
        [XmlIgnore]
        public int ID { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public ReportDB Report { get; set; }

        [XmlIgnore]
        public int ReportID { get; set; }

        [XmlElement(ElementName = "Distance from the centre, m")]
        public double Value { get; set; }
    }
}
