﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("Domain.Models.AppUser", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<string>("MeasurementSystemPreference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Email");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Email = "abc@gmail.com",
                            Gender = "Male",
                            HashedPassword = "abc",
                            Height = 60.0,
                            MeasurementSystemPreference = "Imperial",
                            Salt = "abc",
                            Weight = 120.0
                        });
                });

            modelBuilder.Entity("Domain.Models.BodyMeasurement", b =>
                {
                    b.Property<int>("BodyMeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AppUserEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("BodyFatPercentage")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<double?>("HipCircumference")
                        .HasColumnType("REAL");

                    b.Property<double>("NeckCircumference")
                        .HasColumnType("REAL");

                    b.Property<double>("WaistCircumference")
                        .HasColumnType("REAL");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("BodyMeasurementId");

                    b.HasIndex("AppUserEmail");

                    b.ToTable("BodyMeasurements");

                    b.HasData(
                        new
                        {
                            BodyMeasurementId = 1,
                            AppUserEmail = "abc@gmail.com",
                            BodyFatPercentage = 10.0,
                            DateAdded = new DateTime(2020, 5, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            NeckCircumference = 12.0,
                            WaistCircumference = 28.0,
                            Weight = 125.0
                        },
                        new
                        {
                            BodyMeasurementId = 2,
                            AppUserEmail = "abc@gmail.com",
                            BodyFatPercentage = 10.0,
                            DateAdded = new DateTime(2020, 5, 6, 0, 0, 0, 0, DateTimeKind.Local),
                            NeckCircumference = 12.0,
                            WaistCircumference = 28.0,
                            Weight = 125.0
                        },
                        new
                        {
                            BodyMeasurementId = 3,
                            AppUserEmail = "abc@gmail.com",
                            BodyFatPercentage = 10.0,
                            DateAdded = new DateTime(2020, 5, 7, 0, 0, 0, 0, DateTimeKind.Local),
                            NeckCircumference = 12.0,
                            WaistCircumference = 28.0,
                            Weight = 125.0
                        });
                });

            modelBuilder.Entity("Domain.Models.BodyMeasurement", b =>
                {
                    b.HasOne("Domain.Models.AppUser", "AppUser")
                        .WithMany("BodyMeasurements")
                        .HasForeignKey("AppUserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
