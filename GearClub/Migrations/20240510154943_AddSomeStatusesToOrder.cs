using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearClub.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeStatusesToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCheckedOut",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCheckedOut",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "Orders");
        }
    }
}
