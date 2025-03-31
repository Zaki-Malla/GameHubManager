using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSV2.Migrations
{
    /// <inheritdoc />
    public partial class NewRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Devices_DeviceModelId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_DeviceModelId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DeviceModelId",
                table: "Reservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceModelId",
                table: "Reservations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DeviceModelId",
                table: "Reservations",
                column: "DeviceModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Devices_DeviceModelId",
                table: "Reservations",
                column: "DeviceModelId",
                principalTable: "Devices",
                principalColumn: "Id");
        }
    }
}
