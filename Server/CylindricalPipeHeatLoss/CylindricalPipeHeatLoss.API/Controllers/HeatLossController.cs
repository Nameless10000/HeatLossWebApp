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
        // Удаление материала, если его нет в отчетах
        [HttpDelete]
        public async Task<JsonResult> RemoveMaterial([FromQuery] int materialId)
        {
            return new(new
            {
                Message = await dBAccessService.RemoveUnusedMaterialAsync(materialId)
                    ? "Материал успешно удален"
                    : "Материал не существует или используется"
            });
        }

        // Удаление группы материалов, если она пустая
        [HttpDelete]
        public async Task<JsonResult> RemoveMaterialGroup([FromQuery] int materialGroupId)
        {
            return new(new
            {
                Message = await dBAccessService.RemoveUnusedMaterialGroupAsync(materialGroupId)
                    ? "Группа успешно удалена"
                    : "Группа не существует или содержит материалы"
            });
        }

        // Получение только групп, без материалов (мб счетчик материалов в группе)
        [HttpGet]
        public async Task<JsonResult> GetMaterialWithCounterGroups()
        {
            var groups = await dBAccessService.GetMaterialGroupsAsync();
            var withCounter = groups
                .Select(x => new { x.ID, x.Name, MaterialsCount = x.Materials.Count })
                .ToList();
            return new(withCounter);
        }

        public async Task<JsonResult> AddMaterialGroup([FromBody] MaterialGroupDTO materialGroupDTO)
        {
            var materialGroup = await dBAccessService.AddMaterialGroupAsync(materialGroupDTO);

            if (materialGroup == null)
                return new(new { Message = "Группа с таким именем уже существует" });

            return new(new { materialGroup.ID, materialGroup.Name, MaterialsCount = materialGroup.Materials.Count });
        }

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
        public async Task<JsonResult> GetReport([FromQuery] int id)
        {
            return new(await dBAccessService.GetReportAsync(id));
        }

        [HttpGet]
        public async Task<JsonResult> GetMaterialsForSelector()
        {
            return new((await dBAccessService.GetMaterialsAsync())
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
        public async Task<JsonResult> GetMaterials([FromQuery] int groupID = -1)
        {
            return new(await dBAccessService.GetMaterialsAsync(groupID));
        }

        [HttpPost]
        public async Task<JsonResult> AddMaterial([FromBody] MaterialDTO materialDTO)
        {
            return new(await dBAccessService.AddMaterialAsync(materialDTO));
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
