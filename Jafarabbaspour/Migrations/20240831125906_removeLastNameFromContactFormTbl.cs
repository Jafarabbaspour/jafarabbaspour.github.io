using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jafarabbaspour.Migrations
{
    /// <inheritdoc />
    public partial class removeLastNameFromContactFormTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ContactForms");

            migrationBuilder.RenameColumn(
                name: "RoleIdTeam",
                table: "Portfolios",
                newName: "RoleInTeam");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ContactForms",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleInTeam",
                table: "Portfolios",
                newName: "RoleIdTeam");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "ContactForms",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ContactForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
