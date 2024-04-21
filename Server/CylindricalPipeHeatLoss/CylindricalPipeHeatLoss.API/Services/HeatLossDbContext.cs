using CylindricalPipeHeatLoss.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class HeatLossDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<RadiusDB> Radiuses { get; set; }

        public DbSet<TemperatureDB> Temperatures { get; set; }

        public DbSet<MaterialDB> Materials { get; set; }

        public DbSet<PipeLayerDB> PipeLayers { get; set; }

        public DbSet<MaterialGroupDB> MaterialGroups { get; set; }

        public DbSet<ReportDB> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ReportDB>()
                .Property(x => x.ID)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<ReportDB>()
                .HasMany(x => x.Temperatures)
                .WithOne(x => x.Report)
                .HasForeignKey(x => new { x.ReportID });

            modelBuilder
                .Entity<ReportDB>()
                .HasMany(x => x.Radiuses)
                .WithOne(x => x.Report)
                .HasForeignKey(x => new { x.ReportID });

            modelBuilder
                .Entity<ReportDB>()
                .HasMany(x => x.PipeLayers)
                .WithOne(x => x.Report)
                .HasForeignKey(x => new { x.ReportID });

            modelBuilder
                .Entity<PipeLayerDB>()
                .HasOne(x => x.Material)
                .WithMany(x => x.PipeLayers)
                .HasForeignKey(x => new { x.MaterialID });

            modelBuilder
                .Entity<MaterialDB>()
                .HasOne(x => x.MaterialGroup)
                .WithMany(x => x.Materials)
                .HasForeignKey(x => new { x.MaterialGroupID });

            modelBuilder
                .Entity<MaterialGroupDB>()
                .HasData(
                    new MaterialGroupDB()
                    {
                        ID = 1,
                        Name = "Шамотные огнеупоры общего назначения"
                    },
                    new MaterialGroupDB()
                    {
                        ID = 2,
                        Name = "Шамоты легковесные"
                    }, 
                    new MaterialGroupDB()
                    {
                        ID = 3,
                        Name = "Корундовые обычные"
                    }, 
                    new MaterialGroupDB()
                    {
                        ID = 4,
                        Name = "Корундовые легковесы"
                    }, 
                    new MaterialGroupDB()
                    {
                        ID = 5,
                        Name = "Магнезитовые (периклазовые)"
                    },
                    new MaterialGroupDB()
                    {
                        ID = 6,
                        Name = "Периклазошпинелидные хромитовые"
                    },
                    new MaterialGroupDB()
                    {
                        ID = 7,
                        Name = "Хромитопериклазовые"
                    },
                    new MaterialGroupDB()
                    {
                        ID = 8,
                        Name = "Вата"
                    },
                    new MaterialGroupDB()
                    {
                        ID = 9,
                        Name = "Рулонный материал"
                    },
                    new MaterialGroupDB()
                    {
                        ID = 10,
                        Name = "Войлок"
                    },
                    new MaterialGroupDB()
                    {
                        ID = 11,
                        Name = "Плиты на органической связке"
                    },
                    new MaterialGroupDB()
                    {
                        ID = 12,
                        Name = "Плиты на неорганической связке"
                    },
                    new MaterialGroupDB()
                    {
                        ID = 13,
                        Name = "Бумага"
                    },
                    new MaterialGroupDB()
                    {
                        ID = 14,
                        Name = "Картон"
                    }
                );

            modelBuilder
                .Entity<MaterialDB>()
                .HasData(
                    new MaterialDB
                    {
                        ID = 1,
                        MaterialGroupID = 1,
                        Name = "ША",
                        ACoeff = -0.009e-6,
                        BCoeff = 0.372e-3,
                        CCoeff = 0.974
                    },
                    new MaterialDB
                    {
                        ID = 2,
                        MaterialGroupID = 1,
                        Name = "ШБ",
                        ACoeff = 0,
                        BCoeff = 0.59e-3,
                        CCoeff = 0.804
                    },
                    new MaterialDB
                    {
                        ID = 3,
                        MaterialGroupID = 2,
                        Name = "ШЛ-1,3",
                        ACoeff = 0,
                        BCoeff = 0.35e-3,
                        CCoeff = 0.47
                    },
                    new MaterialDB
                    {
                        ID = 4,
                        MaterialGroupID = 2,
                        Name = "ШКЛ-1,3",
                        ACoeff = 0,
                        BCoeff = 0.347e-3,
                        CCoeff = 0.49
                    },
                    new MaterialDB
                    {
                        ID = 5,
                        MaterialGroupID = 2,
                        Name = "ШЛ-1,0",
                        ACoeff = 0,
                        BCoeff = 0.351e-3,
                        CCoeff = 0.35
                    },
                    new MaterialDB
                    {
                        ID = 6,
                        MaterialGroupID = 2,
                        Name = "ШЛА-1,3",
                        ACoeff = -0.009e-6,
                        BCoeff = 0.238e-3,
                        CCoeff = 0.47
                    },
                    new MaterialDB
                    {
                        ID = 7,
                        MaterialGroupID = 2,
                        Name = "ШЛА-1,0",
                        ACoeff = -0.027e-6,
                        BCoeff = 0.227e-3,
                        CCoeff = 0.307
                    },
                    new MaterialDB
                    {
                        ID = 8,
                        MaterialGroupID = 2,
                        Name = "ШЛ-0,9",
                        ACoeff = -0.009e-6,
                        BCoeff = 0.205e-3,
                        CCoeff = 0.316
                    },
                    new MaterialDB
                    {
                        ID = 9,
                        MaterialGroupID = 2,
                        Name = "ШЛ-0,6",
                        ACoeff = -0.045e-6,
                        BCoeff = 0.176e-3,
                        CCoeff = 0.206
                    },
                    new MaterialDB
                    {
                        ID = 10,
                        MaterialGroupID = 2,
                        Name = "ШЛ-0,4",
                        ACoeff = -0.018e-6,
                        BCoeff = 0.192e-3,
                        CCoeff = 0.119
                    },
                    new MaterialDB
                    {
                        ID = 11,
                        MaterialGroupID = 3,
                        Name = "К",
                        ACoeff = 1.452e-6,
                        BCoeff = -4.398e-3,
                        CCoeff = 6.040
                    },
                    new MaterialDB
                    {
                        ID = 13,
                        MaterialGroupID = 4,
                        Name = "КЛ-1,3",
                        ACoeff = 0.1e-6,
                        BCoeff = -0.259e-3,
                        CCoeff = 0.897
                    },
                    new MaterialDB
                    {
                        ID = 14,
                        MaterialGroupID = 5,
                        Name = "МО-91",
                        ACoeff = 2.2e-6,
                        BCoeff = -6.1e-3,
                        CCoeff = 6.84
                    },
                    new MaterialDB
                    {
                        ID = 15,
                        MaterialGroupID = 5,
                        Name = "М-3",
                        ACoeff = 2.12e-6,
                        BCoeff = -5.9e-3,
                        CCoeff = 6.61
                    },
                    new MaterialDB
                    {
                        ID = 16,
                        MaterialGroupID = 5,
                        Name = "МУ-89",
                        ACoeff = 4.37e-6,
                        BCoeff = -12.4e-3,
                        CCoeff = 12.1
                    },
                    new MaterialDB
                    {
                        ID = 17,
                        MaterialGroupID = 5,
                        Name = "М-4",
                        ACoeff = 5.02e-6,
                        BCoeff = -11e-3,
                        CCoeff = 10.6
                    },
                    new MaterialDB
                    {
                        ID = 18,
                        MaterialGroupID = 5,
                        Name = "М-6",
                        ACoeff = 4.48e-6,
                        BCoeff = -11.3e-3,
                        CCoeff = 12.1
                    },
                    new MaterialDB
                    {
                        ID = 19,
                        MaterialGroupID = 5,
                        Name = "М-7",
                        ACoeff = 4.4e-6,
                        BCoeff = -11.7e-3,
                        CCoeff = 12.2
                    },
                    new MaterialDB
                    {
                        ID = 20,
                        MaterialGroupID = 5,
                        Name = "М-8",
                        ACoeff = 4.4e-6,
                        BCoeff = -11.7e-3,
                        CCoeff = 12.1
                    },
                    new MaterialDB
                    {
                        ID = 21,
                        MaterialGroupID = 5,
                        Name = "М-9",
                        ACoeff = 4.5e-6,
                        BCoeff = -11.8e-3,
                        CCoeff = 12.0
                    },
                    new MaterialDB
                    {
                        ID = 22,
                        MaterialGroupID = 5,
                        Name = "МУ-91",
                        ACoeff = 5.59e-6,
                        BCoeff = -14.9e-3,
                        CCoeff = 14.0
                    },
                    new MaterialDB
                    {
                        ID = 23,
                        MaterialGroupID = 5,
                        Name = "МУ-92",
                        ACoeff = 4.96e-6,
                        BCoeff = -14.8e-3,
                        CCoeff = 14.4
                    },
                    new MaterialDB
                    {
                        ID = 24,
                        MaterialGroupID = 5,
                        Name = "МО-89",
                        ACoeff = 2.2e-6,
                        BCoeff = -6.1e-3,
                        CCoeff = 6.84
                    },
                    new MaterialDB
                    {
                        ID = 25,
                        MaterialGroupID = 5,
                        Name = "МГ",
                        ACoeff = 4.41e-6,
                        BCoeff = -11.8e-3,
                        CCoeff = 12.0
                    },
                    new MaterialDB
                    {
                        ID = 26,
                        MaterialGroupID = 6,
                        Name = "ПХСП",
                        ACoeff = 0.24e-6,
                        BCoeff = -1.31e-3,
                        CCoeff = 4.25
                    },
                    new MaterialDB
                    {
                        ID = 27,
                        MaterialGroupID = 6,
                        Name = "ПХСУТ",
                        ACoeff = 0.98e-6,
                        BCoeff = -3.33e-3,
                        CCoeff = 4.77
                    },
                    new MaterialDB
                    {
                        ID = 28,
                        MaterialGroupID = 6,
                        Name = "ПХСУ",
                        ACoeff = 0.97e-6,
                        BCoeff = -3.31e-3,
                        CCoeff = 4.72
                    },
                    new MaterialDB
                    {
                        ID = 29,
                        MaterialGroupID = 6,
                        Name = "ПХСОТ",
                        ACoeff = 0.37e-6,
                        BCoeff = -0.9e-3,
                        CCoeff = 2.82
                    },
                    new MaterialDB
                    {
                        ID = 30,
                        MaterialGroupID = 6,
                        Name = "ПХСС",
                        ACoeff = 0.41e-6,
                        BCoeff = -0.91e-3,
                        CCoeff = 2.98
                    },
                    new MaterialDB
                    {
                        ID = 31,
                        MaterialGroupID = 7,
                        Name = "ХМ1-1",
                        ACoeff = 0.43e-6,
                        BCoeff = -1.15e-3,
                        CCoeff = 2.68
                    },
                    new MaterialDB
                    {
                        ID = 32,
                        MaterialGroupID = 7,
                        Name = "ХМ2-1",
                        ACoeff = 0.42e-6,
                        BCoeff = -1.11e-3,
                        CCoeff = 2.66
                    },
                    new MaterialDB
                    {
                        ID = 33,
                        MaterialGroupID = 7,
                        Name = "ХМ3-1",
                        ACoeff = 0.42e-6,
                        BCoeff = -1.1e-3,
                        CCoeff = 2.64
                    },
                    new MaterialDB
                    {
                        ID = 34,
                        MaterialGroupID = 7,
                        Name = "ХМ3-2",
                        ACoeff = 0.37e-6,
                        BCoeff = -1.01e-3,
                        CCoeff = 2.36
                    },
                    new MaterialDB
                    {
                        ID = 35,
                        MaterialGroupID = 7,
                        Name = "ХМ4-1",
                        ACoeff = 0.38e-6,
                        BCoeff = -1.01e-3,
                        CCoeff = 2.32
                    },
                    new MaterialDB
                    {
                        ID = 36,
                        MaterialGroupID = 8,
                        Name = "МКРВ-80",
                        ACoeff = 0.231e-6,
                        BCoeff = 0.168e-3,
                        CCoeff = 0.046
                    },
                    new MaterialDB
                    {
                        ID = 37,
                        MaterialGroupID = 9,
                        Name = "МКРР-130",
                        ACoeff = 0.149e-6,
                        BCoeff = 0.136e-3,
                        CCoeff = 0.047
                    },
                    new MaterialDB
                    {
                        ID = 38,
                        MaterialGroupID = 9,
                        Name = "МКРРХ-150",
                        ACoeff = 0.132e-6,
                        BCoeff = 0.122e-3,
                        CCoeff = 0.049
                    },
                    new MaterialDB
                    {
                        ID = 39,
                        MaterialGroupID = 9,
                        Name = "МКЦ-150",
                        ACoeff = 0.132e-6,
                        BCoeff = 0.122e-3,
                        CCoeff = 0.049
                    },
                    new MaterialDB
                    {
                        ID = 40,
                        MaterialGroupID = 10,
                        Name = "МКРВ-200",
                        ACoeff = 0.106e-6,
                        BCoeff = 0.096e-3,
                        CCoeff = 0.055
                    },
                    new MaterialDB
                    {
                        ID = 41,
                        MaterialGroupID = 11,
                        Name = "МКРП-340",
                        ACoeff = 0.07e-6,
                        BCoeff = 0.069e-3,
                        CCoeff = 0.079
                    },
                    new MaterialDB
                    {
                        ID = 42,
                        MaterialGroupID = 12,
                        Name = "МКРП-450",
                        ACoeff = 0.02e-6,
                        BCoeff = 0.159e-3,
                        CCoeff = 0.126
                    },
                    new MaterialDB
                    {
                        ID = 43,
                        MaterialGroupID = 13,
                        Name = "МКРБ-500",
                        ACoeff = 0.05e-6,
                        BCoeff = 0.068e-3,
                        CCoeff = 0.108
                    },
                    new MaterialDB
                    {
                        ID = 44,
                        MaterialGroupID = 14,
                        Name = "МКРК-500",
                        ACoeff = 0.05e-6,
                        BCoeff = 0.068e-3,
                        CCoeff = 0.108
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
