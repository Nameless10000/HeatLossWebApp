using CylindricalPipeHeatLoss.API.Models.DBModels;
using CylindricalPipeHeatLoss.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Math;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class DBAccessService(HeatLossDbContext dbContext)
    {
        #region Report Access Block

        public async Task<List<ReportDB>> GetReportsAsync(ReportsGetParams getParams)
        {
            var reportsByDate = dbContext.Reports
                .Include(x => x.Temperatures)
                .Include(x => x.PipeLayers)
                .ThenInclude(x => x.Material)
                .Include(x => x.Radiuses)
                .Where(report => report.GeneratedAt > getParams.From && report.GeneratedAt < getParams.To)
                .AsQueryable();

            if (getParams.Ql.HasValue)
                reportsByDate = reportsByDate
                    .Where(report => Abs(report.ql - getParams.Ql.Value) <= getParams.QlPrecision)
                    .AsQueryable();

            return await reportsByDate.ToListAsync();
        }

        #endregion

        #region DB Dictionaries Access Block

        public async Task<List<MaterialDB>> GetMaterialsAsync(int groupId = -1)
        {
            return await (groupId switch
            {
                -1 => dbContext.Materials.Include(x => x.MaterialGroup).ToListAsync(),
                _ => dbContext.Materials
                    .Include(x => x.MaterialGroup)
                    .Where(material => material.MaterialGroupID == groupId)
                    .ToListAsync()
            });
        }

        public async Task<List<MaterialGroupDB>> GetMaterialGroupsAsync()
        {
            return await dbContext.MaterialGroups.ToListAsync();
        }

        public async Task<List<PipeLayerDB>> GetLayersByReport(int reportId)
        {
            return await dbContext.PipeLayers
                .Where(layer => layer.ReportID == reportId)
                .ToListAsync();
        }

        public async Task<List<TemperatureDB>> GetTempsByReport(int reportId)
        {
            return await dbContext.Temperatures
                .Where(layer => layer.ReportID == reportId)
                .ToListAsync();
        }

        #endregion
    }
}
