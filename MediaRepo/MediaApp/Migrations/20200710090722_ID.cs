using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaApp.Migrations
{
    public partial class ID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    MediaId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    UserScore = table.Column<int>(nullable: false),
                    PublishedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.UserId, x.MediaId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
