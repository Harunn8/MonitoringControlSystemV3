using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class NewLogDeviceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    ParameterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParameterName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsFixed = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Threshold = table.Column<string>(type: "text", nullable: true),
                    Condition = table.Column<string>(type: "text", nullable: true),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scripts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ScriptName = table.Column<string>(type: "text", nullable: true),
                    Script = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scripts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnmpDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    Port = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnmpDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TcpDevices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    TcpFormat = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TcpDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OidMapping",
                columns: table => new
                {
                    ParameterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Oid = table.Column<string>(type: "text", nullable: true),
                    ParameterName = table.Column<string>(type: "text", nullable: true),
                    SnmpDeviceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OidMapping", x => x.ParameterId);
                    table.ForeignKey(
                        name: "FK_OidMapping_SnmpDevices_SnmpDeviceId",
                        column: x => x.SnmpDeviceId,
                        principalTable: "SnmpDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TcpData",
                columns: table => new
                {
                    ParameterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Request = table.Column<string>(type: "text", nullable: true),
                    ParameterName = table.Column<string>(type: "text", nullable: true),
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
                name: "IX_OidMapping_SnmpDeviceId",
                table: "OidMapping",
                column: "SnmpDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_TcpData_TcpDeviceId",
                table: "TcpData",
                column: "TcpDeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "OidMapping");

            migrationBuilder.DropTable(
                name: "Scripts");

            migrationBuilder.DropTable(
                name: "TcpData");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SnmpDevices");

            migrationBuilder.DropTable(
                name: "TcpDevices");
        }
    }
}
