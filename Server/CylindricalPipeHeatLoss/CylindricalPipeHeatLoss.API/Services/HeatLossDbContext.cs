using CylindricalPipeHeatLoss.API.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace CylindricalPipeHeatLoss.API.Services
{
    public class HeatLossDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<DiameterDB> Diameters { get; set; }

        public DbSet<TemperatureDB> Temperatures { get; set; }

        public DbSet<ThermalResistanceDB> ThermalResistances { get; set; }

        public DbSet<PipeLayerDB> PipeLayers { get; set; }

        public DbSet<ReportDB> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ReportDB>()
                .HasMany(x => x.Temperatures)
                .WithOne(x => x.Report)
                .HasForeignKey(x => new { x.ReportID, x.ReportGeneratedAt });

            modelBuilder
                .Entity<ReportDB>()
                .HasMany(x => x.Diameters)
                .WithOne(x => x.Report)
                .HasForeignKey(x => new { x.ReportID, x.ReportGeneratedAt });

            modelBuilder
                .Entity<ReportDB>()
                .HasMany(x => x.ThermalResistances)
                .WithOne(x => x.Report)
                .HasForeignKey(x => new { x.ReportID, x.ReportGeneratedAt });

            modelBuilder
                .Entity<ReportDB>()
                .HasMany(x => x.PipeLayers)
                .WithOne(x => x.Report)
                .HasForeignKey(x => new { x.ReportID, x.ReportGeneratedAt });

            base.OnModelCreating(modelBuilder);
        }
    }
}
