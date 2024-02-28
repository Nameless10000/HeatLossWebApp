using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class MaterialGroupDB
    {
        [Key]
        public int ID {  get; set; }

        [Column(TypeName = "VARCHAR(128)")]
        public string Name { get; set; }

        public List<MaterialDB> Materials { get; set; }
    }
}
