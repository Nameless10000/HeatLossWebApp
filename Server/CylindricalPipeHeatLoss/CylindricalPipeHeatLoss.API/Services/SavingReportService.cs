﻿using CylindricalPipeHeatLoss.API.Models;
using CylindricalPipeHeatLoss.API.Models.DBModels;
using CylindricalPipeHeatLoss.API.Models.DTOs;
using CylindricalPipeHeatLoss.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Serialization;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class SavingReportService(ReportModelGeneratingService reportGeneratingService, HeatLossDbContext dbContext)
    {
        public async Task<Stream> SaveReportAs(int reportID, FileType fileType = FileType.Xml)
        {
            return await Task.Run(async () =>
            {
                var report = await dbContext.Reports
                .Include(r => r.Temperatures)
                .Include(r => r.Radiuses)
                .Include(r => r.PipeLayers)
                .ThenInclude(r => r.Material)
                .ThenInclude(m => m.MaterialGroup)
                .FirstOrDefaultAsync(r => r.ID == reportID) ?? throw new ArgumentOutOfRangeException($"There are not any reports with given ({reportID}) ID");

                switch (fileType)
                {
                    case FileType.Xml:
                        {
                            var xmlSerializer = new XmlSerializer(typeof(ReportDB), [typeof(TemperatureDB), typeof(RadiusDB), typeof(PipeLayerDB), typeof(MaterialDB), typeof(MaterialGroupDB)]);

                            var ms = new MemoryStream();

                            xmlSerializer.Serialize(ms, report);

                            return ms;
                        }/*
                    case FileType.Excel:
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            var ms = new MemoryStream();

                            using var package = new ExcelPackage(ms);
                            var worksheet = package.Workbook.Worksheets.Add("Heat loss report");

                            worksheet.Cells[1, 1].Value = "Тепловые потери";
                            worksheet.Cells[2, 1].Value = "Q, Вт";
                            worksheet.Cells[3, 1].Value = report.Q;
                            
                            worksheet.Cells[1, 2].Value = "Длина трубы";
                            worksheet.Cells[2, 2].Value = "l, м";
                            worksheet.Cells[3, 2].Value = report.PipeLength;

                            worksheet.Cells[1, 3].Value = "Коэффициент теплоотдачи";
                            worksheet.Cells[2, 3].Value = "α";
                            worksheet.Cells[3, 3].Value = report.a2;

                            worksheet.Cells[1, 4].Value = "Степень черноты поверхности внешней стенки";
                            worksheet.Cells[2, 4].Value = "ε";
                            worksheet.Cells[3, 4].Value = report.e;

                            worksheet.Cells[1, 5].Value = "Линейная (погонная) плотность теплового потока через цилиндрическую стенку";
                            worksheet.Cells[2, 5].Value = "ql, Вт/м";
                            worksheet.Cells[3, 5].Value = report.ql;

                            // формирование блока рассчетных температур
                            var tempHeaderCells = worksheet.Cells[1, 6, 1, 6 + report.Temperatures.Count];
                            tempHeaderCells.Value = "Температурные характеристики, C";
                            tempHeaderCells.Merge = true;
                            worksheet.Cells[2, 6].Value = "Tf1";
                            worksheet.Cells[2, 7].Value = "Tw1";
                            var i = 7;
                            for (; i < 4 + report.Temperatures.Count;)
                                worksheet.Cells[2, ++i].Value = $"Tl{i - 6}-{i - 5}";

                            worksheet.Cells[2, ++i].Value = "Tw2";
                            worksheet.Cells[2, ++i].Value = "Tf2";

                            List<double> temps = [report.InnerTemp, .. report.Temperatures.Select(t => t.Value), report.OutterTemp];
                            for (var j = 6; j < 6 + temps.Count; j++)
                                worksheet.Cells[3, j].Value = temps[j - 6];

                            // Конец блока температур

                            // Формирование блока информации по слоям

                            var layersHeaderCells = worksheet.Cells[1, ++i, 1, i + 5];
                            layersHeaderCells.Value = "Информация по слоям";
                            layersHeaderCells.Merge = true;

                            var row = 2;
                            var layerNum = 0;
                            foreach (var layer in report.PipeLayers)
                            {
                                layerNum++;
                                var layerHeader = worksheet.Cells[row, i, row, i + 5];
                                layerHeader.Value = $"Слой №{layerNum} ({layer.Material.Name})";
                                layerHeader.Merge = true;

                                worksheet.Cells[row + 1, i].Value = "A";
                                worksheet.Cells[row + 2, i].Value = layer.Material.ACoeff;
                                worksheet.Cells[row + 1, i + 1].Value = "B";
                                worksheet.Cells[row + 2, i + 1].Value = layer.Material.BCoeff;
                                worksheet.Cells[row + 1, i + 2].Value = "C";
                                worksheet.Cells[row + 2, i + 2].Value = layer.Material.CCoeff;
                                worksheet.Cells[row + 1, i + 3].Value = "Толщина, м";
                                worksheet.Cells[row + 2, i + 3].Value = layer.Width;

                                row += 3;
                            }

                            i += 6; // указатель на следующий пустой столбец

                            // Окончание блока ифнормации по слоям

                            worksheet.Cells[1, i].Value = "Плотность теплового потока от внутренней среды к стенке";
                            worksheet.Cells[2, i].Value = "inner ql, Вт/м";
                            worksheet.Cells[3, i].Value = report.InnerQl;

                            i++;

                            worksheet.Cells[1, i].Value = "Плотность теплового потока от наружной стенки в окружающую среду";
                            worksheet.Cells[2, i].Value = "outter ql, Вт/м";
                            worksheet.Cells[3, i].Value = report.OutterQl;

                            i++;

                            worksheet.Cells.AutoFitColumns();
                            
                            package.Save();

                            return ms;
                        }*/
                    default:
                        return new MemoryStream();
                        
                }
                
            });
        }
    }
}
