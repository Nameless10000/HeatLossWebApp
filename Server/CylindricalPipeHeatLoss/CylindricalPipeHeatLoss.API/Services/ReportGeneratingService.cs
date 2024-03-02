﻿using CylindricalPipeHeatLoss.Library.Models;
using CylindricalPipeHeatLoss.Library;
using CylindricalPipeHeatLoss.API.Models.DTOs;
using AutoMapper;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class ReportGeneratingService(IMapper mapper)
    {
        public ReportModel CalculateHeatLossInfo(HeatLossRequestDTO requestDTO) 
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
                 

            return lib.GetReport();
        }
    }
}
