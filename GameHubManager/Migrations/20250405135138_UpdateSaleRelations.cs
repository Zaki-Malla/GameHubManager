using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSV2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSaleRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "MenuSales",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuSales_MenuItemId",
                table: "MenuSales",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSales_MenuItems_MenuItemId",
                table: "MenuSales",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuSales_MenuItems_MenuItemId",
                table: "MenuSales");

            migrationBuilder.DropIndex(
                name: "IX_MenuSales_MenuItemId",
                table: "MenuSales");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "MenuSales");
        }
    }
}
