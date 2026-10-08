using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazynApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dodanoPrzedmiot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LokalizacjaId",
                table: "Przedmioty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Przedmioty_LokalizacjaId",
                table: "Przedmioty",
                column: "LokalizacjaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmioty_Lokalizacje_LokalizacjaId",
                table: "Przedmioty",
                column: "LokalizacjaId",
                principalTable: "Lokalizacje",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Przedmioty_Lokalizacje_LokalizacjaId",
                table: "Przedmioty");

            migrationBuilder.DropIndex(
                name: "IX_Przedmioty_LokalizacjaId",
                table: "Przedmioty");

            migrationBuilder.DropColumn(
                name: "LokalizacjaId",
                table: "Przedmioty");
        }
    }
}
