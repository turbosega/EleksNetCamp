using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class ChangeResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameOutcome",
                table: "Results");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Results",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Results",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Results");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Results",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<string>(
                name: "GameOutcome",
                table: "Results",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
