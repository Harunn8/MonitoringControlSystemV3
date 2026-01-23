using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class NewVersionSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnmpDevices");

            migrationBuilder.DropTable(
                name: "TcpData");

            migrationBuilder.DropTable(
                name: "ParameterModel");

            migrationBuilder.DropTable(
                name: "TcpDevices");

            migrationBuilder.CreateTable(
                name: "PagDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PagId = table.Column<Guid>(type: "uuid", nullable: false),
                    PagDeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    Timeout = table.Column<int>(type: "integer", nullable: false),
                    Retry = table.Column<int>(type: "integer", nullable: false),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParameterLogsTs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParameterSetsName = table.Column<string>(type: "text", nullable: true),
                    ParameterId = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    DeviceId = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    Interval = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterLogsTs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommunicationType = table.Column<int>(type: "integer", nullable: false),
                    BrandName = table.Column<string>(type: "text", nullable: true),
                    ModelName = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<string>(type: "text", nullable: true),
                    CommunicationData = table.Column<string>(type: "text", nullable: true),
                    AlarmsId = table.Column<Guid>(type: "uuid", nullable: true),
                    PagDevicesId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Alarms_AlarmsId",
                        column: x => x.AlarmsId,
                        principalTable: "Alarms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_PagDevices_PagDevicesId",
                        column: x => x.PagDevicesId,
                        principalTable: "PagDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_AlarmsId",
                table: "Devices",
                column: "AlarmsId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_PagDevicesId",
                table: "Devices",
                column: "PagDevicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Pags");

            migrationBuilder.DropTable(
                name: "ParameterLogsTs");

            migrationBuilder.DropTable(
                name: "PagDevices");

            migrationBuilder.CreateTable(
                name: "ParameterModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Oid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TcpDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    PagId = table.Column<Guid>(type: "uuid", nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    TcpFormat = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TcpDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnmpDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    ParametersId = table.Column<Guid>(type: "uuid", nullable: true),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    ReadCommunity = table.Column<string>(type: "text", nullable: true),
                    Retry = table.Column<int>(type: "integer", nullable: false),
                    SnmpVersion = table.Column<int>(type: "integer", nullable: false),
                    Timeout = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: true),
                    WriteCommunity = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnmpDevices_ParameterModel_ParametersId",
                        column: x => x.ParametersId,
                        principalTable: "ParameterModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TcpData",
                columns: table => new
                {
                    ParameterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParameterName = table.Column<string>(type: "text", nullable: true),
                    Request = table.Column<string>(type: "text", nullable: true),
                    TcpDeviceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TcpData", x => x.ParameterId);
                    table.ForeignKey(
                        name: "FK_TcpData_TcpDevices_TcpDeviceId",
                        column: x => x.TcpDeviceId,
                        principalTable: "TcpDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SnmpDevices_ParametersId",
                table: "SnmpDevices",
                column: "ParametersId");

            migrationBuilder.CreateIndex(
                name: "IX_TcpData_TcpDeviceId",
                table: "TcpData",
                column: "TcpDeviceId");
        }
    }
}
