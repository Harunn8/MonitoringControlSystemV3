using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class NewVersionSchemaV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_PagDevices_PagDevicesId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_PagDevicesId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "PagDevicesId",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_PagDevices_DeviceId",
                table: "PagDevices",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PagDevices_Devices_DeviceId",
                table: "PagDevices",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PagDevices_Devices_DeviceId",
                table: "PagDevices");

            migrationBuilder.DropIndex(
                name: "IX_PagDevices_DeviceId",
                table: "PagDevices");

            migrationBuilder.AddColumn<Guid>(
                name: "PagDevicesId",
                table: "Devices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_PagDevicesId",
                table: "Devices",
                column: "PagDevicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_PagDevices_PagDevicesId",
                table: "Devices",
                column: "PagDevicesId",
                principalTable: "PagDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
