using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSV2.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "MenuItems",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "MenuItems");
        }
    }
}
