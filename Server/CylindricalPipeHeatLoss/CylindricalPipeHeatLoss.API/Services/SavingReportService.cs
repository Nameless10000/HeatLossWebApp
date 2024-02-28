using CylindricalPipeHeatLoss.API.Models;
using CylindricalPipeHeatLoss.Library.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Serialization;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class SavingReportService(ReportGeneratingService reportGeneratingService)
    {
        public async Task<Stream> SaveReportAs(HeatLossRequestDTO requestDTO, FileType fileType = FileType.Xml)
        {
            return await Task.Run(() =>
            {
                var reportData = reportGeneratingService.CalculateHeatLossInfo(requestDTO);
                switch (fileType)
                {
                    case FileType.Xml:
                        {
                            var xmlSerializer = new XmlSerializer(typeof(ReportModel));

                            var ms = new MemoryStream();

                            xmlSerializer.Serialize(ms, reportData);

                            return ms;
                        }
                    case FileType.Excel:
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            var ms = new MemoryStream();

                            using var package = new ExcelPackage(ms);
                            var worksheet = package.Workbook.Worksheets.Add("Heat loss report");

                            worksheet.Cells[1, 1].Value = "Тепловые потери";
                            worksheet.Cells[2, 1].Value = "Q, Вт";
                            worksheet.Cells[3, 1].Value = reportData.Q;

                            worksheet.Cells[1, 2].Value = "Коэффициент теплоотдачи";
                            worksheet.Cells[2, 2].Value = "α";
                            worksheet.Cells[3, 2].Value = reportData.a2;

                            /*worksheet.Cells[1, 3].Value = "Линейный коэффициент теплопередачи";
                            worksheet.Cells[2, 3].Value = "kl, Вт/(м*К)";
                            worksheet.Cells[3, 3].Value = reportData.kl;*/ // TODO: заменить kl на что-либо другое

                            worksheet.Cells[1, 4].Value = "Линейная (погонная) плотность теплового потока через цилиндрическую стенку";
                            worksheet.Cells[2, 4].Value = "ql, Вт/м";
                            worksheet.Cells[3, 4].Value = reportData.ql;

                            worksheet.Cells[1, 5, 1, 4 + reportData.Temperatures.Count].Value = "Температурные характеристики, C";
                            worksheet.Cells[1, 5, 1, 4 + reportData.Temperatures.Count].Merge = true;
                            worksheet.Cells[2, 5].Value = "Tf1";
                            worksheet.Cells[2, 6].Value = "Tw1";
                            var i = 6;
                            for (; i < 2 + reportData.Temperatures.Count;)
                                worksheet.Cells[2, ++i].Value = $"Tl{i - 4}-{i - 3}";

                            worksheet.Cells[2, ++i].Value = "Tw2";
                            worksheet.Cells[2, ++i].Value = "Tf2";

                            /*for (var j = 5; j < 5 + reportData.Temperatures.Count; j++)
                                worksheet.Cells[3, j].Value = reportData.Temperatures[j - 5];*/ // TODO: Температуры сред не в reportData.Temperatures, надо вынести j за for...

                            worksheet.Cells.AutoFitColumns();

                            package.Save();

                            return ms;
                        }
                    default:
                        return new MemoryStream();
                        
                }
                
            });
        }
    }
}
