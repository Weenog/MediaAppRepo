using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaApp.Migrations
{
    public partial class NoDummy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dummy",
                table: "medias");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dummy",
                table: "medias",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
