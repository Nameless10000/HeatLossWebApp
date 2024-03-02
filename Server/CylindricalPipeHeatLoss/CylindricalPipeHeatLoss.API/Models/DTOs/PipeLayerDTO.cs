namespace CylindricalPipeHeatLoss.API.Models.DTOs
{
    public class PipeLayerDTO
    {
        public int? MaterialID { get; set; }

        public double Width { get; set; }

        public bool IsResourceMaterial => MaterialID != null && MaterialID > 0;

        public double? ACoeff { get; set; }

        public double? BCoeff { get; set; }
        
        public double? CCoeff { get; set; }

        public string? MaterialName { get; set; }
    }
}
