using CylindricalPipeHeatLoss.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CylindricalPipeHeatLoss.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        

        var heatLossDbContextConnectionString = builder.Configuration.GetConnectionString("HeatLossConnectionString");
        builder.Services.AddDbContext<HeatLossDbContext>(opt => opt.UseSqlite(heatLossDbContextConnectionString));

        builder.Services.AddTransient<ReportGeneratingService>();
        builder.Services.AddTransient<SavingReportService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
