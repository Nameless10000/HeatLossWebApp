using CylindricalPipeHeatLoss.Library.Models;
using CylindricalPipeHeatLoss.Library;
using CylindricalPipeHeatLoss.API.Models;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class ReportGeneratingService()
    {
        public ReportModel CalculateHeatLossInfo(HeatLossRequestDTO requestDTO) 
        {
            var lib = new CylindricalPipeHeatLossLib(
                requestDTO.InnerPipeDiameter, 
                requestDTO.PipeLength, 
                requestDTO.PipeLayers, 
                requestDTO.A1, 
                requestDTO.A2, 
                requestDTO.Temps);

            return lib.GetReport();
        }
    }
}
