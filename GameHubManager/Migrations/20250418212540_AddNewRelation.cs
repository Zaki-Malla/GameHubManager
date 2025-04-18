using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSV2.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_GroupReservations_GroupReservationId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_GroupReservations_GroupReservationId",
                table: "Reservations",
                column: "GroupReservationId",
                principalTable: "GroupReservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_GroupReservations_GroupReservationId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_GroupReservations_GroupReservationId",
                table: "Reservations",
                column: "GroupReservationId",
                principalTable: "GroupReservations",
                principalColumn: "Id");
        }
    }
}
