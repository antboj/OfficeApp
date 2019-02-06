using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OfficeApp.Migrations
{
    public partial class druga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "usedTo",
                table: "Usages",
                newName: "UsedTo");

            migrationBuilder.RenameColumn(
                name: "usedFrom",
                table: "Usages",
                newName: "UsedFrom");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UsedTo",
                table: "Usages",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsedTo",
                table: "Usages",
                newName: "usedTo");

            migrationBuilder.RenameColumn(
                name: "UsedFrom",
                table: "Usages",
                newName: "usedFrom");

            migrationBuilder.AlterColumn<DateTime>(
                name: "usedTo",
                table: "Usages",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
