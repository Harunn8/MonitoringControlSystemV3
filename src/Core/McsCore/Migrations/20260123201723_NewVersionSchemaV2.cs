using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class NewVersionSchemaV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Alarms_AlarmsId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_AlarmsId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "AlarmsId",
                table: "Devices");

            migrationBuilder.AddColumn<Guid>(
                name: "BaseDeviceModelId",
                table: "Alarms",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_BaseDeviceModelId",
                table: "Alarms",
                column: "BaseDeviceModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_Devices_BaseDeviceModelId",
                table: "Alarms",
                column: "BaseDeviceModelId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_Devices_BaseDeviceModelId",
                table: "Alarms");

            migrationBuilder.DropIndex(
                name: "IX_Alarms_BaseDeviceModelId",
                table: "Alarms");

            migrationBuilder.DropColumn(
                name: "BaseDeviceModelId",
                table: "Alarms");

            migrationBuilder.AddColumn<Guid>(
                name: "AlarmsId",
                table: "Devices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AlarmsId",
                table: "Devices",
                column: "AlarmsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Alarms_AlarmsId",
                table: "Devices",
                column: "AlarmsId",
                principalTable: "Alarms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
