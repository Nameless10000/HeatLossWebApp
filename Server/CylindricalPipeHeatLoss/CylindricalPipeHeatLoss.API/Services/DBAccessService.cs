using CylindricalPipeHeatLoss.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using static System.Math;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class DBAccessService(HeatLossDbContext dbContext)
    {
        #region Report Access Block

        public async Task<List<ReportDB>> GetReportsByDate(DateTime from, DateTime to)
        {
            return await dbContext.Reports
                .Where(report => report.GeneratedAt > from && report.GeneratedAt < to)
                .ToListAsync();
        }

        public async Task<List<ReportDB>> GetReportsByHeatLoss(double ql, double precision)
        {
            return (await dbContext.Reports
                .ToListAsync())
                .Where(report => Abs(report.ql - ql) <= precision)
                .ToList();
        }

        #endregion

        #region DB Dictionaries Access Block

        public async Task<List<MaterialDB>> GetMaterials(int groupId = -1)
        {
            return await (groupId switch
            {
                -1 => dbContext.Materials.ToListAsync(),
                _ => dbContext.Materials
                    .Where(material => material.MaterialGroupID == groupId)
                    .ToListAsync()
            });
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

        /*public async Task<List<MaterialGroupDB>> GetGroups()
        {
            return await dbContext.
        }*/

        #endregion
    }
}
