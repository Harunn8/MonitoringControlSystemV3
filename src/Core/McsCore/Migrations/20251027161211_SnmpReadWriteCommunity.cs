using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class SnmpReadWriteCommunity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReadCommunity",
                table: "SnmpDevices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WriteCommunity",
                table: "SnmpDevices",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadCommunity",
                table: "SnmpDevices");

            migrationBuilder.DropColumn(
                name: "WriteCommunity",
                table: "SnmpDevices");
        }
    }
}
