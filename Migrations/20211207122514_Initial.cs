using Microsoft.EntityFrameworkCore.Migrations;

namespace BP_CalHFA.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalHFADB",
                columns: table => new
                {
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    LoanID = table.Column<int>(type: "int", nullable: false),
                    LoanCategoryID = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalHFADB");
        }
    }
}
