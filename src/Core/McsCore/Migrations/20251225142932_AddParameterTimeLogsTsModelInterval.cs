using Microsoft.EntityFrameworkCore.Migrations;

namespace McsCore.Migrations
{
    public partial class AddParameterTimeLogsTsModelInterval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Interval",
                table: "ParameterLogsTs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "ParameterLogsTs");
        }
    }
}
