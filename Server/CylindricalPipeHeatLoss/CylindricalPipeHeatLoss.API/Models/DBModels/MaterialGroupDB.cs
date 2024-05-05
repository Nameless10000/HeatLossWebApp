using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class MaterialGroupDB
    {
        [Key]
        public int ID {  get; set; }

        [Column(TypeName = "VARCHAR(128)")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<MaterialDB> Materials { get; set; }
    }
}
