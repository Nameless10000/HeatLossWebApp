using AutoMapper;
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

        var dbContext = builder.Services.BuildServiceProvider().GetService<HeatLossDbContext>();

        var mapperProfile = new MapperConfiguration(x => x.AddProfile(new MapperProfile(dbContext)));

        var mapper = mapperProfile.CreateMapper();

        builder.Services.AddSingleton(mapper);

        builder.Services.AddTransient<ReportModelGeneratingService>();
        builder.Services.AddTransient<SavingReportService>();
        builder.Services.AddTransient<DBAccessService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors();

        

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(c => c
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
        ); 
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
