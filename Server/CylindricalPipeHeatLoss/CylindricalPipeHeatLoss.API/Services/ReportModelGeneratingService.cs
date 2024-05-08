using CylindricalPipeHeatLoss.Library.Models;
using CylindricalPipeHeatLoss.Library;
using CylindricalPipeHeatLoss.API.Models.DTOs;
using AutoMapper;
using CylindricalPipeHeatLoss.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class ReportModelGeneratingService(IMapper mapper, HeatLossDbContext dbContext)
    {
        public async Task<ReportDB> CalculateHeatLossInfoAsync(HeatLossRequestDTO requestDTO) 
        {
            var lib = new CylindricalPipeHeatLossLib(
                requestDTO.InnerPipeRadius, 
                requestDTO.A1, 
                requestDTO.E, 
                requestDTO.PipeLayers.Select(mapper.Map<PipeLayer>).ToList(), 
                requestDTO.InnerTemp,
                requestDTO.OutterTemp,
                requestDTO.Precision,
                requestDTO.PipeOrientation,
                requestDTO.PipeLength);
                 
            var report = lib.GetReport();

            var reportDb = mapper.Map<ReportDB>(report);
            reportDb.GeneratedAt = DateTime.Now;

            await dbContext.Reports.AddAsync(reportDb);

            await dbContext.SaveChangesAsync();

            var dbTemps = report.Temperatures.Select(temp => new TemperatureDB
            {
                Value = temp,
                ReportID = reportDb.ID
            });

            await dbContext.Temperatures.AddRangeAsync(dbTemps);

            var dbRadiuses = report.Radiuses.Select(rad => new RadiusDB
            {
                Value = rad,
                ReportID = reportDb.ID
            });

            await dbContext.Radiuses.AddRangeAsync(dbRadiuses);

            var dbLayers = requestDTO.PipeLayers
                .Select(layer =>
                {
                    if (layer.IsResourceMaterial)
                        return new PipeLayerDB
                        {
                            MaterialID = layer.MaterialID!.Value,
                            ReportID = reportDb.ID,
                            Width = layer.Width
                        };

                    var newMaterial = new MaterialDB
                    {
                        ACoeff = layer.ACoeff ?? 0,
                        BCoeff = layer.BCoeff ?? 0,
                        CCoeff = layer.CCoeff ?? 0,
                        Name = layer.MaterialName!,
                        MaterialGroupID = layer.MaterialGroupID!.Value
                    };

                    dbContext.Materials.Add(newMaterial);
                    dbContext.SaveChanges();

                    return new PipeLayerDB
                    {
                        MaterialID = newMaterial.ID,
                        ReportID = reportDb.ID,
                        Width = layer.Width
                    };
                });

            await dbContext.PipeLayers.AddRangeAsync(dbLayers);

            await dbContext.SaveChangesAsync();

            return await dbContext.Reports
                .Include(r => r.Radiuses)
                .Include(r => r.Temperatures)
                .Include(r => r.PipeLayers)
                .ThenInclude(layer => layer.Material)
                .FirstAsync(r => r.ID == reportDb.ID);
        }
    }
}
