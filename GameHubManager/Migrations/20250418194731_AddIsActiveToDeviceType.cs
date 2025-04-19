using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSV2.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToDeviceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DeviceTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DeviceTypes");
        }
    }
}
