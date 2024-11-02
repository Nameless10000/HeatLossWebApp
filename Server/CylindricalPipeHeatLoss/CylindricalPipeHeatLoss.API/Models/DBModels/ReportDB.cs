using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class ReportDB
    {
        [Key]
        [XmlIgnore]
        public int ID { get; set; }

        [XmlElement(ElementName = "Generated at")]
        public DateTime GeneratedAt { get; set; }


        public double Q { get; set; }

        public double a1 { get; set; }

        public double a2 { get; set; }

        public double e { get; set; }

        public double ql { get; set; }

        [XmlElement(ElementName = "Inner temperature, °C")]
        public double InnerTemp { get; set; }

        [XmlElement(ElementName = "Outter temperature, °C")]
        public double OutterTemp { get; set; }

        public List<TemperatureDB> Temperatures { get; set; }

        public List<RadiusDB> Radiuses { get; set; }

        public List<PipeLayerDB> PipeLayers { get; set; }

        public double PipeLength { get; set; }

        public double CriticalDiameter { get; set; }
    }
}
