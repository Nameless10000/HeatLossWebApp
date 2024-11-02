using System.ComponentModel.DataAnnotations;

namespace CylindricalPipeHeatLoss.API.Models.DTOs
{
    public class MaterialGroupDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
