using CylindricalPipeHeatLoss.API.Models;
using CylindricalPipeHeatLoss.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CylindricalPipeHeatLoss.API.Controllers
{
    
    [ApiController, Route("api/[controller]/[action]")]
    public class HeatLossController(ReportGeneratingService reportGeneratingService)
    {
        [HttpPost]
        public async Task<JsonResult> GetHeatLossReport(HeatLossRequestDTO requestDTO)
        {
            return new JsonResult(reportGeneratingService.CalculateHeatLossInfo(requestDTO));
        }
    }
}
