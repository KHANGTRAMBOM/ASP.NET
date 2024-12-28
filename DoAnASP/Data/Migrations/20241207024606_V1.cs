using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnASP.Data.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Albums");

            migrationBuilder.AddColumn<int>(
                name: "GenreID",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreID",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GenreID",
                table: "Songs",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_GenreID",
                table: "Albums",
                column: "GenreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Genre_GenreID",
                table: "Albums",
                column: "GenreID",
                principalTable: "Genre",
                principalColumn: "GenreID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Genre_GenreID",
                table: "Songs",
                column: "GenreID",
                principalTable: "Genre",
                principalColumn: "GenreID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Genre_GenreID",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Genre_GenreID",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Songs_GenreID",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Albums_GenreID",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "GenreID",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "GenreID",
                table: "Albums");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
