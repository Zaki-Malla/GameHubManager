using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSV2.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReservationIdFromSaleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuSales_Reservations_ReservationId",
                table: "MenuSales");

            migrationBuilder.DropIndex(
                name: "IX_MenuSales_ReservationId",
                table: "MenuSales");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "MenuSales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "MenuSales",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuSales_ReservationId",
                table: "MenuSales",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSales_Reservations_ReservationId",
                table: "MenuSales",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }
    }
}
