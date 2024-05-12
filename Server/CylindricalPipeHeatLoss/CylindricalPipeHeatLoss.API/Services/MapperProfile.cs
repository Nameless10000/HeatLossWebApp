using AutoMapper;
using CylindricalPipeHeatLoss.API.Models.DBModels;
using CylindricalPipeHeatLoss.API.Models.DTOs;
using CylindricalPipeHeatLoss.Library.Models;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class MapperProfile : Profile
    {
        private readonly List<MaterialDB> ResourceMaterials;

        public MapperProfile(HeatLossDbContext dbContext)
        {
            ResourceMaterials = dbContext.Materials.ToList();

            CreateMap<PipeLayerDTO, PipeLayer>()
                .ForMember(x => x.Material, opt => opt.MapFrom(x => ExtractMaterialFromPipeLayerDTO(x)));

            CreateMap<ReportModel, ReportDB>()
                .ForMember(x => x.Temperatures, opt => opt.Ignore())
                .ForMember(x => x.Radiuses, opt => opt.Ignore())
                .ForMember(x => x.PipeLayers, opt => opt.Ignore());

            CreateMap<MaterialDTO, MaterialDB>();
        }

        private Material ExtractMaterialFromPipeLayerDTO(PipeLayerDTO pipeLayerDTO)
        {
            if (pipeLayerDTO.IsResourceMaterial)
            {
                var material = ResourceMaterials.FirstOrDefault(x => x.ID == pipeLayerDTO.MaterialID);
                return new Material
                {
                    ACoeff = material.ACoeff,
                    BCoeff = material.BCoeff,
                    CCoeff = material.CCoeff,
                    Name = material.Name
                };
            }

            return new Material
            {
                ACoeff = pipeLayerDTO.ACoeff.Value,
                BCoeff = pipeLayerDTO.BCoeff.Value,
                CCoeff = pipeLayerDTO.CCoeff.Value,
                Name = pipeLayerDTO.MaterialName
            };
        }
    }
}
