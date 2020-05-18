using Microsoft.EntityFrameworkCore.Migrations;

namespace unibook.Migrations
{
    public partial class removerating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
