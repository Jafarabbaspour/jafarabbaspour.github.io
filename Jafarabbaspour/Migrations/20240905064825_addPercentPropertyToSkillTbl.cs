using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jafarabbaspour.Migrations
{
    /// <inheritdoc />
    public partial class addPercentPropertyToSkillTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Percent",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percent",
                table: "Skills");
        }
    }
}
