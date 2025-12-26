using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class noBaseCommunicationModelDevices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterModel_SnmpDevices_SnmpDeviceId",
                table: "ParameterModel");

            migrationBuilder.DropIndex(
                name: "IX_ParameterModel_SnmpDeviceId",
                table: "ParameterModel");

            migrationBuilder.DropColumn(
                name: "PagId",
                table: "SnmpDevices");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PagId",
                table: "SnmpDevices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SnmpDeviceId",
                table: "ParameterModel",
                type: "uuid",
                nullable: true);

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
    }
}
