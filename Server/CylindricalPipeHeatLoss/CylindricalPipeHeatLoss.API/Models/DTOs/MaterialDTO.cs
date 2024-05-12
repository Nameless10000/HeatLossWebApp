namespace CylindricalPipeHeatLoss.API.Models.DTOs;

public class MaterialDTO
{
    public string Name { get; set; }

    public double ACoeff { get; set; }

    public double BCoeff { get; set; }

    public double CCoeff { get; set; }

    public string? MaterialGroupName { get; set; }

    public int? MaterialGroupID { get; set; }
}
