using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazynApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMaxLocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaksLokalizacji",
                table: "Magazyny",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaksLokalizacji",
                table: "Magazyny");
        }
    }
}
