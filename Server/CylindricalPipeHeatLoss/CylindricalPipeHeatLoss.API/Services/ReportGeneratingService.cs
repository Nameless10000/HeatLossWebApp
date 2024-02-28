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
                requestDTO.InnerPipeRadius, 
                requestDTO.A1, 
                requestDTO.E, 
                requestDTO.PipeLayers, 
                requestDTO.InnerTemp,
                requestDTO.OutterTemp,
                requestDTO.Precision,
                requestDTO.PipeOrientation,
                requestDTO.PipeLength);
                 

            return lib.GetReport();
        }
    }
}
