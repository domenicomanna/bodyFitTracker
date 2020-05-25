using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    AppUserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(nullable: true),
                    HashedPassword = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    MeasurementSystemPreference = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.AppUserId);
                });

            migrationBuilder.CreateTable(
                name: "BodyMeasurements",
                columns: table => new
                {
                    BodyMeasurementId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<int>(nullable: false),
                    NeckCircumference = table.Column<double>(nullable: false),
                    WaistCircumference = table.Column<double>(nullable: false),
                    HipCircumference = table.Column<double>(nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    BodyFatPercentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMeasurements", x => x.BodyMeasurementId);
                    table.ForeignKey(
                        name: "FK_BodyMeasurements_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "AppUserId", "Email", "Gender", "HashedPassword", "Height", "MeasurementSystemPreference", "Salt", "Weight" },
                values: new object[] { 1, "abc@gmail.com", "Male", "abc", 60.0, "Imperial", "abc", 120.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 1, 1, 10.0, new DateTime(2020, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), null, 12.0, 28.0, 125.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 2, 1, 10.0, new DateTime(2020, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), null, 12.0, 28.0, 125.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 3, 1, 10.0, new DateTime(2020, 5, 27, 0, 0, 0, 0, DateTimeKind.Local), null, 12.0, 28.0, 125.0 });

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurements_AppUserId",
                table: "BodyMeasurements",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyMeasurements");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
