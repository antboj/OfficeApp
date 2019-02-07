using Microsoft.EntityFrameworkCore.Migrations;

namespace OfficeApp.Migrations
{
    public partial class treca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Persons_PersonId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Devices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Persons_PersonId",
                table: "Devices",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Persons_PersonId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Devices",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Persons_PersonId",
                table: "Devices",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
