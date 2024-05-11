using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class PipeLayerDB
    {
        [Key]
        [XmlIgnore]
        public int ID { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public ReportDB Report { get; set; }

        [XmlIgnore]
        public int ReportID { get; set; }

        [XmlIgnore]
        public int MaterialID { get; set; }

        public MaterialDB Material { get; set; }

        public double Width { get; set; }
    }
}
