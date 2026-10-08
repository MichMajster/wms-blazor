using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazynApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Kontrahent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DostawcaId",
                table: "Przedmioty",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OdbiorcaId",
                table: "Przedmioty",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Przedmioty_DostawcaId",
                table: "Przedmioty",
                column: "DostawcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Przedmioty_OdbiorcaId",
                table: "Przedmioty",
                column: "OdbiorcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmioty_Kontrahenci_DostawcaId",
                table: "Przedmioty",
                column: "DostawcaId",
                principalTable: "Kontrahenci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Przedmioty_Kontrahenci_OdbiorcaId",
                table: "Przedmioty",
                column: "OdbiorcaId",
                principalTable: "Kontrahenci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Przedmioty_Kontrahenci_DostawcaId",
                table: "Przedmioty");

            migrationBuilder.DropForeignKey(
                name: "FK_Przedmioty_Kontrahenci_OdbiorcaId",
                table: "Przedmioty");

            migrationBuilder.DropIndex(
                name: "IX_Przedmioty_DostawcaId",
                table: "Przedmioty");

            migrationBuilder.DropIndex(
                name: "IX_Przedmioty_OdbiorcaId",
                table: "Przedmioty");

            migrationBuilder.DropColumn(
                name: "DostawcaId",
                table: "Przedmioty");

            migrationBuilder.DropColumn(
                name: "OdbiorcaId",
                table: "Przedmioty");
        }
    }
}
