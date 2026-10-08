using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazynApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserAuthentication1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordH");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordH",
                table: "Users",
                newName: "Password");
        }
    }
}
