using CylindricalPipeHeatLoss.API.Models;
using CylindricalPipeHeatLoss.API.Models.DBModels;
using CylindricalPipeHeatLoss.API.Models.DTOs;
using CylindricalPipeHeatLoss.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CylindricalPipeHeatLoss.API.Controllers
{

    [ApiController, Route("api/[controller]/[action]")]
    public class HeatLossController(
        ReportModelGeneratingService reportGeneratingService,
        SavingReportService savingReportService,
        DBAccessService dBAccessService
        ) : Controller
    {
        [HttpPost]
        public async Task<JsonResult> CalcHeatLossReport(HeatLossRequestDTO requestDTO)
        {
            return new JsonResult(await reportGeneratingService.CalculateHeatLossInfoAsync(requestDTO));
        }

        [HttpPost]
        public async Task<JsonResult> GetReports([FromBody] ReportsGetParams reportsGetParams)
        {
            return new(await dBAccessService.GetReportsAsync(reportsGetParams));
        }

        [HttpGet]
        public async Task<JsonResult> GetMaterials([FromQuery] int groupId = -1)
        {
            return new((await dBAccessService.GetMaterialsAsync(groupId))
                .GroupBy(g => g.MaterialGroup.Name)
                .Select(g => new 
                    {
                        title = g.Key,
                        value = g.Key,
                        selectable = false,
                        children = g.Select(m => new {title = m.Name, value = m.ID})
                    }
                ));
        }

        [HttpGet]
        public async Task<JsonResult> GetMaterialGroups()
        {
            return new(await dBAccessService.GetMaterialGroupsAsync());
        }

        [HttpPost]
        public async Task<FileResult> GetXmlReport(int requestID)
        {
            var ms = await savingReportService.SaveReportAs(requestID);

            ms.Position = 0;

            return File(ms, "application/xml", $"report {DateTime.Now:f}.xml");
        }

        /*[HttpPost]
        public async Task<FileResult> GetExcelReport(HeatLossRequestDTO requestDTO)
        {
            var ms = await savingReportService.SaveReportAs(requestDTO, FileType.Excel);

            ms.Position = 0;

            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"report {DateTime.Now:f}.xlsx");
        }*/
    }
}
