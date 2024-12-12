using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetAtlasApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSpendTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SpendTime",
                table: "Expenses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpendTime",
                table: "Expenses");
        }
    }
}
