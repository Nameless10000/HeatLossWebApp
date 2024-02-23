using CylindricalPipeHeatLoss.API.Models;
using CylindricalPipeHeatLoss.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CylindricalPipeHeatLoss.API.Controllers
{
    
    [ApiController, Route("api/[controller]/[action]")]
    public class HeatLossController(
        ReportGeneratingService reportGeneratingService,
        SavingReportService savingReportService
        ) : Controller
    {
        [HttpPost]
        public async Task<JsonResult> GetHeatLossReport(HeatLossRequestDTO requestDTO)
        {
            return new JsonResult(reportGeneratingService.CalculateHeatLossInfo(requestDTO));
        }

        [HttpPost]
        public async Task<FileResult> GetXmlReport(HeatLossRequestDTO requestDTO)
        {
            var ms = await savingReportService.SaveReportAs(requestDTO);

            ms.Position = 0;

            return File(ms, "application/xml", $"report {DateTime.Now:f}.xml");
        }

        [HttpPost]
        public async Task<FileResult> GetExcelReport(HeatLossRequestDTO requestDTO)
        {
            var ms = await savingReportService.SaveReportAs(requestDTO, FileType.Excel);

            ms.Position = 0;

            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"report {DateTime.Now:f}.xlsx");
        }
    }
}
