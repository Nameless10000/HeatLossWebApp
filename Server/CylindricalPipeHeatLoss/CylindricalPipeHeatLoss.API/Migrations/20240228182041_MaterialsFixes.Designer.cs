﻿// <auto-generated />
using System;
using CylindricalPipeHeatLoss.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CylindricalPipeHeatLoss.API.Migrations
{
    [DbContext(typeof(HeatLossDbContext))]
    [Migration("20240228182041_MaterialsFixes")]
    partial class MaterialsFixes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.MaterialDB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("ACoeff")
                        .HasColumnType("REAL");

                    b.Property<double>("BCoeff")
                        .HasColumnType("REAL");

                    b.Property<double>("CCoeff")
                        .HasColumnType("REAL");

                    b.Property<int>("MaterialGroupID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(32)");

                    b.HasKey("ID");

                    b.HasIndex("MaterialGroupID");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.MaterialGroupDB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(128)");

                    b.HasKey("ID");

                    b.ToTable("MaterialGroupDB");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.PipeLayerDB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaterialID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ReportGeneratedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("ReportID")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Width")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.HasIndex("MaterialID");

                    b.HasIndex("ReportID", "ReportGeneratedAt");

                    b.ToTable("PipeLayers");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.RadiusDB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ReportGeneratedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("ReportID")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.HasIndex("ReportID", "ReportGeneratedAt");

                    b.ToTable("Radiuses");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.ReportDB", b =>
                {
                    b.Property<int>("ID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("GeneratedAt")
                        .HasColumnType("TEXT");

                    b.Property<double>("InnerTemp")
                        .HasColumnType("REAL");

                    b.Property<double>("OutterTemp")
                        .HasColumnType("REAL");

                    b.Property<double>("PipeLength")
                        .HasColumnType("REAL");

                    b.Property<double>("Q")
                        .HasColumnType("REAL");

                    b.Property<double>("a1")
                        .HasColumnType("REAL");

                    b.Property<double>("a2")
                        .HasColumnType("REAL");

                    b.Property<double>("e")
                        .HasColumnType("REAL");

                    b.Property<double>("ql")
                        .HasColumnType("REAL");

                    b.HasKey("ID", "GeneratedAt");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.TemperatureDB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ReportGeneratedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("ReportID")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.HasIndex("ReportID", "ReportGeneratedAt");

                    b.ToTable("Temperatures");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.MaterialDB", b =>
                {
                    b.HasOne("CylindricalPipeHeatLoss.API.Models.DBModels.MaterialGroupDB", "MaterialGroup")
                        .WithMany("Materials")
                        .HasForeignKey("MaterialGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaterialGroup");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.PipeLayerDB", b =>
                {
                    b.HasOne("CylindricalPipeHeatLoss.API.Models.DBModels.MaterialDB", "Material")
                        .WithMany("PipeLayers")
                        .HasForeignKey("MaterialID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CylindricalPipeHeatLoss.API.Models.DBModels.ReportDB", "Report")
                        .WithMany("PipeLayers")
                        .HasForeignKey("ReportID", "ReportGeneratedAt")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.RadiusDB", b =>
                {
                    b.HasOne("CylindricalPipeHeatLoss.API.Models.DBModels.ReportDB", "Report")
                        .WithMany("Radiuses")
                        .HasForeignKey("ReportID", "ReportGeneratedAt")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.TemperatureDB", b =>
                {
                    b.HasOne("CylindricalPipeHeatLoss.API.Models.DBModels.ReportDB", "Report")
                        .WithMany("Temperatures")
                        .HasForeignKey("ReportID", "ReportGeneratedAt")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.MaterialDB", b =>
                {
                    b.Navigation("PipeLayers");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.MaterialGroupDB", b =>
                {
                    b.Navigation("Materials");
                });

            modelBuilder.Entity("CylindricalPipeHeatLoss.API.Models.DBModels.ReportDB", b =>
                {
                    b.Navigation("PipeLayers");

                    b.Navigation("Radiuses");

                    b.Navigation("Temperatures");
                });
#pragma warning restore 612, 618
        }
    }
}