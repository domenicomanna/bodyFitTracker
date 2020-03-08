﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200303033122_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Email");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Domain.Models.BodyMeasurement", b =>
                {
                    b.Property<int>("BodyMeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AppUserEmail")
                        .IsRequired()
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