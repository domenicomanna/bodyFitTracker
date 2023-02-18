using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table =>
                    new
                    {
                        AppUserId = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        Email = table.Column<string>(type: "text", nullable: false),
                        HashedPassword = table.Column<string>(type: "text", nullable: false),
                        Salt = table.Column<string>(type: "text", nullable: false),
                        Height = table.Column<double>(type: "double precision", nullable: false),
                        Gender = table.Column<string>(type: "text", nullable: false),
                        MeasurementSystemPreference = table.Column<string>(type: "text", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.AppUserId);
                }
            );

            migrationBuilder.CreateTable(
                name: "BodyMeasurements",
                columns: table =>
                    new
                    {
                        BodyMeasurementId = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        AppUserId = table.Column<int>(type: "integer", nullable: false),
                        NeckCircumference = table.Column<double>(type: "double precision", nullable: false),
                        WaistCircumference = table.Column<double>(type: "double precision", nullable: false),
                        HipCircumference = table.Column<double>(type: "double precision", nullable: true),
                        Height = table.Column<double>(type: "double precision", nullable: false),
                        Weight = table.Column<double>(type: "double precision", nullable: false),
                        DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                        BodyFatPercentage = table.Column<double>(type: "double precision", nullable: false),
                        Units = table.Column<string>(type: "text", nullable: false)
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
            );

            migrationBuilder.CreateTable(
                name: "PasswordResets",
                columns: table =>
                    new
                    {
                        Token = table.Column<string>(type: "text", nullable: false),
                        AppUserId = table.Column<int>(type: "integer", nullable: false),
                        Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
            );

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
