using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBook2.Data.Migrations
{
    public partial class PdfFileColumnAddedToBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePDF",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePDF",
                table: "Books");
        }
    }
}
