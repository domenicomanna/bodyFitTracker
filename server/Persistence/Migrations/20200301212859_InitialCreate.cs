using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(nullable: true),
                    HashedPassword = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Gender = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Email", "Gender", "HashedPassword", "Height", "Salt", "Weight" },
                values: new object[] { 1, "abc", 0, null, 0.0, null, 0.0 });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Email", "Gender", "HashedPassword", "Height", "Salt", "Weight" },
                values: new object[] { 2, "abcc", 0, null, 0.0, null, 0.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
