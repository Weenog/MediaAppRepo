using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaApp.Migrations
{
    public partial class AddCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "medias");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "medias",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Movie" },
                    { 2, "Podcast" },
                    { 3, "Music" },
                    { 4, "Book" },
                    { 5, "Game" },
                    { 6, "Other" },

                });

            migrationBuilder.CreateIndex(
                name: "IX_medias_CategoryId",
                table: "medias",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_medias_Categories_CategoryId",
                table: "medias",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medias_Categories_CategoryId",
                table: "medias");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_medias_CategoryId",
                table: "medias");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "medias");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "medias",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
