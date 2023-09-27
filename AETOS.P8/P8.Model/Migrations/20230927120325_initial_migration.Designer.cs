﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using P8.Model.DbContexts;

#nullable disable

namespace P8.Model.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230927120325_initial_migration")]
    partial class initial_migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("P8.Model.Models.DeviceInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("DeviceInfos");
                });

            modelBuilder.Entity("P8.Model.Models.Temperature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DeviceInfoId")
                        .HasColumnType("integer");

                    b.Property<int>("Psi")
                        .HasColumnType("integer");

                    b.Property<int>("Temp")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceInfoId");

                    b.ToTable("Temperatures");
                });

            modelBuilder.Entity("P8.Model.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Accuracy")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Altitude")
                        .HasColumnType("double precision");

                    b.Property<int>("AssetID")
                        .HasColumnType("integer");

                    b.Property<string>("AssetName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CANSpeed")
                        .HasColumnType("integer");

                    b.Property<int>("CompanyID")
                        .HasColumnType("integer");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DriverID")
                        .HasColumnType("integer");

                    b.Property<string>("DriverName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("EngineHours")
                        .HasColumnType("double precision");

                    b.Property<int>("GroupID")
                        .HasColumnType("integer");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Ignition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Odometer")
                        .HasColumnType("double precision");

                    b.Property<int>("Reason")
                        .HasColumnType("integer");

                    b.Property<long>("SerialNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("Speed")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("TransactionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("P8.Model.Models.Temperature", b =>
                {
                    b.HasOne("P8.Model.Models.DeviceInfo", "DeviceInfo")
                        .WithMany()
                        .HasForeignKey("DeviceInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeviceInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
