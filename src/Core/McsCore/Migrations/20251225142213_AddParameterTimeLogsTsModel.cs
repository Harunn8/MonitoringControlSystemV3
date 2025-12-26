using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class AddParameterTimeLogsTsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SnmpDevices_ParameterModel_ParametersId",
                table: "SnmpDevices");

            migrationBuilder.DropIndex(
                name: "IX_SnmpDevices_ParametersId",
                table: "SnmpDevices");

            migrationBuilder.DropColumn(
                name: "ParametersId",
                table: "SnmpDevices");

            migrationBuilder.AddColumn<Guid>(
                name: "SnmpDeviceId",
                table: "ParameterModel",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ParameterLogsTs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParameterSetsName = table.Column<string>(type: "text", nullable: true),
                    ParameterId = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    DeviceId = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterLogsTs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParameterModel_SnmpDeviceId",
                table: "ParameterModel",
                column: "SnmpDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterModel_SnmpDevices_SnmpDeviceId",
                table: "ParameterModel",
                column: "SnmpDeviceId",
                principalTable: "SnmpDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterModel_SnmpDevices_SnmpDeviceId",
                table: "ParameterModel");

            migrationBuilder.DropTable(
                name: "ParameterLogsTs");

            migrationBuilder.DropIndex(
                name: "IX_ParameterModel_SnmpDeviceId",
                table: "ParameterModel");

            migrationBuilder.DropColumn(
                name: "SnmpDeviceId",
                table: "ParameterModel");

            migrationBuilder.AddColumn<Guid>(
                name: "ParametersId",
                table: "SnmpDevices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SnmpDevices_ParametersId",
                table: "SnmpDevices",
                column: "ParametersId");

            migrationBuilder.AddForeignKey(
                name: "FK_SnmpDevices_ParameterModel_ParametersId",
                table: "SnmpDevices",
                column: "ParametersId",
                principalTable: "ParameterModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
