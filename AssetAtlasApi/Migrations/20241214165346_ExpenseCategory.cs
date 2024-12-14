using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetAtlasApi.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseCategory",
                table: "Expenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseCategory",
                table: "Expenses");
        }
    }
}
