using Microsoft.EntityFrameworkCore.Migrations;

namespace unibook.Migrations
{
    public partial class @decimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Listings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Listings",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
