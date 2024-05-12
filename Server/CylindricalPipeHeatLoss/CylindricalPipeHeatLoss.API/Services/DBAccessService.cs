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

        #endregion

        #region DB Dictionaries Access Block

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

        public async Task<MaterialDB> AddMaterialAsync(MaterialDTO materialDTO)
        {
            var material = mapper.Map<MaterialDB>(materialDTO);

            material.MaterialGroupID = (materialDTO.MaterialGroupID == null 
                || materialDTO.MaterialGroupID == 0)
                && !string.IsNullOrWhiteSpace(materialDTO.MaterialGroupName) 
                    ? (await AddMaterialGroupAsync(materialDTO.MaterialGroupName)).ID
            : materialDTO.MaterialGroupID.Value;

            await dbContext.Materials.AddAsync(material);
            await dbContext.SaveChangesAsync();

            return material;
        }

        private async Task<MaterialGroupDB> AddMaterialGroupAsync(string name)
        {
            var sameGroup = await dbContext.MaterialGroups.FirstOrDefaultAsync(g => g.Name == name);

            if (sameGroup != null)
                return sameGroup;

            var newGroup = new MaterialGroupDB()
            {
                Name = name
            };

            await dbContext.MaterialGroups.AddAsync(newGroup);
            await dbContext.SaveChangesAsync();

            return newGroup;
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
