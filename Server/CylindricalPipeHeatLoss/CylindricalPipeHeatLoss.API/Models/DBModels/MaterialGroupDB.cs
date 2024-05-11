using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class MaterialGroupDB
    {
        [Key]
        [XmlIgnore]
        public int ID {  get; set; }

        [Column(TypeName = "VARCHAR(128)")]
        [XmlElement(ElementName = "Materials group name")]
        public string Name { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public List<MaterialDB> Materials { get; set; }
    }
}
