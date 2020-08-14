using System;
using Microsoft.EntityFrameworkCore.Metadata;
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<int>(nullable: false),
                    NeckCircumference = table.Column<double>(nullable: false),
                    WaistCircumference = table.Column<double>(nullable: false),
                    HipCircumference = table.Column<double>(nullable: true),
                    Height = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    BodyFatPercentage = table.Column<double>(nullable: false),
                    Units = table.Column<string>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "PasswordResets",
                columns: table => new
                {
                    Token = table.Column<string>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResets", x => x.Token);
                    table.ForeignKey(
                        name: "FK_PasswordResets_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "AppUserId", "Email", "Gender", "HashedPassword", "Height", "MeasurementSystemPreference", "Salt" },
                values: new object[] { 1, "mannadomenico2849@gmail.com", "Male", "Xt+eYgLCOWjNy3YBxMWvcDKOQoEVtVwIyCDp9qfo+ag=", 60.0, "Imperial", "HvJRurMKIz+KkIpQhw4DpA==" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "AppUserId", "Email", "Gender", "HashedPassword", "Height", "MeasurementSystemPreference", "Salt" },
                values: new object[] { 2, "bcdf@gmail.com", "Female", "Xt+eYgLCOWjNy3YBxMWvcDKOQoEVtVwIyCDp9qfo+ag=", 55.0, "Imperial", "HvJRurMKIz+KkIpQhw4DpA==" });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserId", "BodyFatPercentage", "DateAdded", "Height", "HipCircumference", "NeckCircumference", "Units", "WaistCircumference", "Weight" },
                values: new object[,]
                {
                    { 1, 1, 10.0, new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Local), 60.0, null, 12.0, "Imperial", 28.0, 125.0 },
                    { 2, 1, 10.0, new DateTime(2020, 8, 13, 0, 0, 0, 0, DateTimeKind.Local), 60.0, null, 12.0, "Imperial", 28.0, 120.0 },
                    { 3, 1, 10.0, new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), 60.0, null, 12.0, "Imperial", 28.0, 130.0 },
                    { 4, 1, 10.0, new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), 60.0, null, 12.0, "Imperial", 28.0, 145.0 },
                    { 5, 1, 10.0, new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), 60.0, null, 12.0, "Imperial", 28.0, 115.0 },
                    { 6, 2, 10.0, new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), 60.0, 20.0, 12.0, "Imperial", 28.0, 121.0 },
                    { 7, 2, 10.0, new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), 60.0, 20.0, 12.0, "Imperial", 28.0, 122.0 },
                    { 8, 2, 10.0, new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), 60.0, 20.0, 10.0, "Imperial", 30.0, 125.0 },
                    { 9, 2, 12.0, new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), 60.0, 20.0, 12.0, "Imperial", 28.0, 126.59999999999999 },
                    { 10, 2, 11.0, new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Local), 60.0, 20.0, 11.0, "Imperial", 29.0, 125.90000000000001 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Email",
                table: "AppUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurements_AppUserId",
                table: "BodyMeasurements",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResets_AppUserId",
                table: "PasswordResets",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyMeasurements");

            migrationBuilder.DropTable(
                name: "PasswordResets");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
