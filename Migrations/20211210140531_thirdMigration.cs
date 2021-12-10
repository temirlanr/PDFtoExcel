using Microsoft.EntityFrameworkCore.Migrations;

namespace PDFtoExcel.Migrations
{
    public partial class thirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Files",
                newName: "MyFileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyFileName",
                table: "Files",
                newName: "FileName");
        }
    }
}
