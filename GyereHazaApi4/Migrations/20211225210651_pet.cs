using Microsoft.EntityFrameworkCore.Migrations;

namespace GyereHazaApi4.Migrations
{
    public partial class pet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Pets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Pets",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Pets");
        }
    }
}
