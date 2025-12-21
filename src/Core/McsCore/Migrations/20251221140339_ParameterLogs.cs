using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class ParameterLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParameterLogs",
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
                    table.PrimaryKey("PK_ParameterLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParameterLogs");
        }
    }
}
