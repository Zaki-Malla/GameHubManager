using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSV2.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupReservationId",
                table: "Reservations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupReservationModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalDevices = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupReservationModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupReservationModel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GroupReservationId",
                table: "Reservations",
                column: "GroupReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupReservationModel_UserId",
                table: "GroupReservationModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_GroupReservationModel_GroupReservationId",
                table: "Reservations",
                column: "GroupReservationId",
                principalTable: "GroupReservationModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_GroupReservationModel_GroupReservationId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "GroupReservationModel");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_GroupReservationId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "GroupReservationId",
                table: "Reservations");
        }
    }
}
