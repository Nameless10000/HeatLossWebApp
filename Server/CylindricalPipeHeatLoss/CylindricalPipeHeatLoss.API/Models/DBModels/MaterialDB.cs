using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class MaterialDB
    {
        [Key]
        public int ID { get; set; }

        public MaterialGroupDB MaterialGroup { get; set; }

        public int MaterialGroupID { get; set; }

        public double ACoeff { get; set; }

        public double BCoeff { get; set; }

        public double CCoeff { get; set; }

        [Column(TypeName = "VARCHAR(32)")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<PipeLayerDB> PipeLayers { get; set; }
    }
}
