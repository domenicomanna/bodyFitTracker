using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase().Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "AppUsers",
                    columns: table =>
                        new
                        {
                            AppUserId = table
                                .Column<int>(type: "int", nullable: false)
                                .Annotation(
                                    "MySql:ValueGenerationStrategy",
                                    MySqlValueGenerationStrategy.IdentityColumn
                                ),
                            Email = table
                                .Column<string>(type: "varchar(255)", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4"),
                            HashedPassword = table
                                .Column<string>(type: "longtext", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4"),
                            Salt = table
                                .Column<string>(type: "longtext", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4"),
                            Height = table.Column<double>(type: "double", nullable: false),
                            Gender = table
                                .Column<string>(type: "longtext", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4"),
                            MeasurementSystemPreference = table
                                .Column<string>(type: "longtext", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4")
                        },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AppUsers", x => x.AppUserId);
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "BodyMeasurements",
                    columns: table =>
                        new
                        {
                            BodyMeasurementId = table
                                .Column<int>(type: "int", nullable: false)
                                .Annotation(
                                    "MySql:ValueGenerationStrategy",
                                    MySqlValueGenerationStrategy.IdentityColumn
                                ),
                            AppUserId = table.Column<int>(type: "int", nullable: false),
                            NeckCircumference = table.Column<double>(type: "double", nullable: false),
                            WaistCircumference = table.Column<double>(type: "double", nullable: false),
                            HipCircumference = table.Column<double>(type: "double", nullable: true),
                            Height = table.Column<double>(type: "double", nullable: false),
                            Weight = table.Column<double>(type: "double", nullable: false),
                            DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                            BodyFatPercentage = table.Column<double>(type: "double", nullable: false),
                            Units = table
                                .Column<string>(type: "longtext", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4")
                        },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_BodyMeasurements", x => x.BodyMeasurementId);
                        table.ForeignKey(
                            name: "FK_BodyMeasurements_AppUsers_AppUserId",
                            column: x => x.AppUserId,
                            principalTable: "AppUsers",
                            principalColumn: "AppUserId",
                            onDelete: ReferentialAction.Cascade
                        );
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder
                .CreateTable(
                    name: "PasswordResets",
                    columns: table =>
                        new
                        {
                            Token = table
                                .Column<string>(type: "varchar(255)", nullable: false)
                                .Annotation("MySql:CharSet", "utf8mb4"),
                            AppUserId = table.Column<int>(type: "int", nullable: false),
                            Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_PasswordResets", x => x.Token);
                        table.ForeignKey(
                            name: "FK_PasswordResets_AppUsers_AppUserId",
                            column: x => x.AppUserId,
                            principalTable: "AppUsers",
                            principalColumn: "AppUserId",
                            onDelete: ReferentialAction.Cascade
                        );
                    }
                )
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(name: "IX_AppUsers_Email", table: "AppUsers", column: "Email", unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurements_AppUserId",
                table: "BodyMeasurements",
                column: "AppUserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResets_AppUserId",
                table: "PasswordResets",
                column: "AppUserId"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "BodyMeasurements");

            migrationBuilder.DropTable(name: "PasswordResets");

            migrationBuilder.DropTable(name: "AppUsers");
        }
    }
}
