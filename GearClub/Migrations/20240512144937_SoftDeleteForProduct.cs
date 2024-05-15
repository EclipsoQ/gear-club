using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearClub.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeleteForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted_at",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified_at",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Deleted_at",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Modified_at",
                table: "Products");
        }
    }
}
