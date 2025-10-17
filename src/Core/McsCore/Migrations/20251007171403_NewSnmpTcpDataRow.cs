using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class NewSnmpTcpDataRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Retry",
                table: "SnmpDevices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SnmpVersion",
                table: "SnmpDevices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Timeout",
                table: "SnmpDevices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "SnmpDevices",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Retry",
                table: "SnmpDevices");

            migrationBuilder.DropColumn(
                name: "SnmpVersion",
                table: "SnmpDevices");

            migrationBuilder.DropColumn(
                name: "Timeout",
                table: "SnmpDevices");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "SnmpDevices");
        }
    }
}
