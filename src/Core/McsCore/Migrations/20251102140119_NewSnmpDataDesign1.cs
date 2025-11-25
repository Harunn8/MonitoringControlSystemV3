using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class NewSnmpDataDesign1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OidMapping");

            migrationBuilder.CreateTable(
                name: "ParameterModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Oid = table.Column<string>(type: "text", nullable: true),
                    SnmpDeviceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterModel_SnmpDevices_SnmpDeviceId",
                        column: x => x.SnmpDeviceId,
                        principalTable: "SnmpDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParameterModel_SnmpDeviceId",
                table: "ParameterModel",
                column: "SnmpDeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParameterModel");

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

            migrationBuilder.CreateIndex(
                name: "IX_OidMapping_SnmpDeviceId",
                table: "OidMapping",
                column: "SnmpDeviceId");
        }
    }
}
