using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaApp.Migrations
{
    public partial class Dummy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dummy",
                table: "medias",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dummy",
                table: "medias");
        }
    }
}
