﻿// <auto-generated />
using System;
using Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Persistence.Migrations
{
    [DbContext(typeof(BodyFitTrackerContext))]
    [Migration("20220320171831_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Api.Domain.Models.AppUser", b =>
                {
                    b.Property<int>("AppUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Height")
                        .HasColumnType("double");

                    b.Property<string>("MeasurementSystemPreference")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AppUserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Api.Domain.Models.BodyMeasurement", b =>
                {
                    b.Property<int>("BodyMeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<double>("BodyFatPercentage")
                        .HasColumnType("double");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Height")
                        .HasColumnType("double");

                    b.Property<double?>("HipCircumference")
                        .HasColumnType("double");

                    b.Property<double>("NeckCircumference")
                        .HasColumnType("double");

                    b.Property<string>("Units")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("WaistCircumference")
                        .HasColumnType("double");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("BodyMeasurementId");

                    b.HasIndex("AppUserId");

                    b.ToTable("BodyMeasurements");
                });

            modelBuilder.Entity("Api.Domain.Models.PasswordReset", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Token");

                    b.HasIndex("AppUserId");

                    b.ToTable("PasswordResets");
                });

            modelBuilder.Entity("Api.Domain.Models.BodyMeasurement", b =>
                {
                    b.HasOne("Api.Domain.Models.AppUser", "AppUser")
                        .WithMany("BodyMeasurements")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Api.Domain.Models.PasswordReset", b =>
                {
                    b.HasOne("Api.Domain.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Api.Domain.Models.AppUser", b =>
                {
                    b.Navigation("BodyMeasurements");
                });
#pragma warning restore 612, 618
        }
    }
}
