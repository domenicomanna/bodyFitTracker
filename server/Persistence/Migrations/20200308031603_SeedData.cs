using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Email", "Gender", "HashedPassword", "Height", "Salt", "Weight" },
                values: new object[] { "abc@gmail.com", "Male", "abc", 60.0, "abc", 120.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserEmail", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 1, "abc@gmail.com", null, 12.0, 30.0, 130.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserEmail", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 2, "abc@gmail.com", null, 12.0, 28.0, 125.0 });

            migrationBuilder.InsertData(
                table: "BodyMeasurements",
                columns: new[] { "BodyMeasurementId", "AppUserEmail", "HipCircumference", "NeckCircumference", "WaistCircumference", "Weight" },
                values: new object[] { 3, "abc@gmail.com", null, 12.0, 26.0, 120.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BodyMeasurements",
                keyColumn: "BodyMeasurementId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BodyMeasurements",
                keyColumn: "BodyMeasurementId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BodyMeasurements",
                keyColumn: "BodyMeasurementId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Email",
                keyValue: "abc@gmail.com");
        }
    }
}
