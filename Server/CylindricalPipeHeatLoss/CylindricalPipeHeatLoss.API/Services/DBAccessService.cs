using AutoMapper;
using CylindricalPipeHeatLoss.API.Models.DBModels;
using CylindricalPipeHeatLoss.API.Models.DTOs;
using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Math;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class DBAccessService(HeatLossDbContext dbContext, IMapper mapper)
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

        public async Task<ReportDB> GetReportAsync(int reportID)
        {
            return await dbContext.Reports
                .Include(x => x.Temperatures)
                .Include(x => x.PipeLayers)
                .ThenInclude(x => x.Material)
                .Include(x => x.Radiuses)
                .FirstOrDefaultAsync(r => r.ID == reportID);
        }

        #endregion

        #region DB Dictionaries Access Block

        public async Task<bool> RemoveUnusedMaterialAsync(int  materialId)
        {
            var material = await dbContext.Materials.FirstOrDefaultAsync(x => x.ID == materialId);

            if (material == null
                || await dbContext.Reports.AnyAsync(x => x.PipeLayers.Any(l => l.MaterialID == materialId)))
                return false;
            
            dbContext.Materials.Remove(material);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<MaterialDB>> GetMaterialsAsync(int groupId = -1)
        {
            return await (groupId switch
            {
                -1 => dbContext.Materials
                    .Include(x => x.MaterialGroup)
                    .ToListAsync(),
                _ => dbContext.Materials
                    .Include(x => x.MaterialGroup)
                    .Where(material => material.MaterialGroupID == groupId)
                    .ToListAsync()
            });
        }

        public async Task<MaterialDB?> AddMaterialAsync(MaterialDTO materialDTO)
        {
            var desiredGroup = await dbContext.MaterialGroups.FirstOrDefaultAsync(x => x.ID == materialDTO.MaterialGroupID);

            if (desiredGroup == null)
                return null;

            var material = mapper.Map<MaterialDB>(materialDTO);

            await dbContext.Materials.AddAsync(material);
            await dbContext.SaveChangesAsync();

            return material;
        }

        public async Task<bool> RemoveUnusedMaterialGroupAsync(int groupId)
        {
            var group = await dbContext.MaterialGroups
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(x => x.ID == groupId);

            if (group == null
                || group.Materials.Count > 0)
                return false;

            dbContext.MaterialGroups.Remove(group);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<MaterialGroupDB?> AddMaterialGroupAsync(MaterialGroupDTO groupDTO)
        {
            var sameGroup = await dbContext.MaterialGroups.FirstOrDefaultAsync(g => g.Name == groupDTO.Name);

            if (sameGroup != null)
                return null;

            var newGroup = mapper.Map<MaterialGroupDB>(groupDTO);

            await dbContext.MaterialGroups.AddAsync(newGroup);
            await dbContext.SaveChangesAsync();

            return await dbContext.MaterialGroups
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(x => x.ID == newGroup.ID);
        }

        public async Task<List<MaterialGroupDB>> GetMaterialGroupsAsync()
        {
            return await dbContext.MaterialGroups.Include(x => x.Materials).ToListAsync();
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
