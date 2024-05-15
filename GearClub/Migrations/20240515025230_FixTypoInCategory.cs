using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearClub.Migrations
{
    /// <inheritdoc />
    public partial class FixTypoInCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Create_at",
                table: "Categories",
                newName: "Created_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "Categories",
                newName: "Create_at");
        }
    }
}
