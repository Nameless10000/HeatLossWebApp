using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class MaterialDB
    {
        [Key]
        [XmlIgnore]
        public int ID { get; set; }

        public MaterialGroupDB MaterialGroup { get; set; }

        [XmlIgnore]
        public int MaterialGroupID { get; set; }

        public double ACoeff { get; set; }

        public double BCoeff { get; set; }

        public double CCoeff { get; set; }

        [Column(TypeName = "VARCHAR(32)")]
        [XmlElement(ElementName = "Material name")]
        public string Name { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public List<PipeLayerDB> PipeLayers { get; set; }
    }
}
