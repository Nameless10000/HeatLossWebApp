﻿using CylindricalPipeHeatLoss.API.Models;
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
