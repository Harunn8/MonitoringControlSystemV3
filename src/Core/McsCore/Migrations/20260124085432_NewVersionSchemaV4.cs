using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class NewVersionSchemaV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PagDeviceId",
                table: "PagDevices",
                newName: "DeviceId");

            migrationBuilder.AddColumn<string>(
                name: "PagDeviceName",
                table: "PagDevices",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagDeviceName",
                table: "PagDevices");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "PagDevices",
                newName: "PagDeviceId");
        }
    }
}
