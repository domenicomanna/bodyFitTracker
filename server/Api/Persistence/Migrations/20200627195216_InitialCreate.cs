using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Persistence.migrations
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
                    Email = table.Column<string>(nullable: false),
                    HashedPassword = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: false),
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
                columns: new[] { "AppUserId", "Email", "Gender", "HashedPassword", "Height", "MeasurementSystemPreference", "Salt" },
                values: new object[] { 1, "abc@gmail.com", "Male", "Xt+eYgLCOWjNy3YBxMWvcDKOQoEVtVwIyCDp9qfo+ag=", 60.0, "Imperial", "HvJRurMKIz+KkIpQhw4DpA==" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "AppUserId", "Email", "Gender", "HashedPassword", "Height", "MeasurementSystemPreference", "Salt" },
                values: new object[] { 2, "bcdf@gmail.com", "Female", "Xt+eYgLCOWjNy3YBxMWvcDKOQoEVtVwIyCDp9qfo+ag=", 55.0, "Imperial", "HvJRurMKIz+KkIpQhw4DpA==" });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 1, 1, 10.0, new DateTime(2020, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), null, 12.0, 28.0, 125.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 2, 1, 10.0, new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Local), null, 12.0, 28.0, 120.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 3, 1, 10.0, new DateTime(2020, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), null, 12.0, 28.0, 130.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 4, 1, 10.0, new DateTime(2020, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), null, 12.0, 28.0, 145.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 5, 1, 10.0, new DateTime(2020, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), null, 12.0, 28.0, 115.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 6, 2, 10.0, new DateTime(2020, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), 20.0, 12.0, 28.0, 121.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 7, 2, 10.0, new DateTime(2020, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), 20.0, 12.0, 28.0, 122.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 8, 2, 10.0, new DateTime(2020, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), 20.0, 10.0, 30.0, 125.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 9, 2, 12.0, new DateTime(2020, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), 20.0, 12.0, 28.0, 126.59999999999999 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 10, 2, 11.0, new DateTime(2020, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), 20.0, 11.0, 29.0, 125.90000000000001 });

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
