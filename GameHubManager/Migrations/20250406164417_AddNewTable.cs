using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSV2.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupReservationModel_AspNetUsers_UserId",
                table: "GroupReservationModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_GroupReservationModel_GroupReservationId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupReservationModel",
                table: "GroupReservationModel");

            migrationBuilder.RenameTable(
                name: "GroupReservationModel",
                newName: "GroupReservations");

            migrationBuilder.RenameIndex(
                name: "IX_GroupReservationModel_UserId",
                table: "GroupReservations",
                newName: "IX_GroupReservations_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupReservations",
                table: "GroupReservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupReservations_AspNetUsers_UserId",
                table: "GroupReservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_GroupReservations_GroupReservationId",
                table: "Reservations",
                column: "GroupReservationId",
                principalTable: "GroupReservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupReservations_AspNetUsers_UserId",
                table: "GroupReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_GroupReservations_GroupReservationId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupReservations",
                table: "GroupReservations");

            migrationBuilder.RenameTable(
                name: "GroupReservations",
                newName: "GroupReservationModel");

            migrationBuilder.RenameIndex(
                name: "IX_GroupReservations_UserId",
                table: "GroupReservationModel",
                newName: "IX_GroupReservationModel_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupReservationModel",
                table: "GroupReservationModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupReservationModel_AspNetUsers_UserId",
                table: "GroupReservationModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_GroupReservationModel_GroupReservationId",
                table: "Reservations",
                column: "GroupReservationId",
                principalTable: "GroupReservationModel",
                principalColumn: "Id");
        }
    }
}
