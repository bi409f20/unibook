using Microsoft.EntityFrameworkCore.Migrations;

namespace unibook.Migrations
{
    public partial class ListingImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListingImage",
                table: "Listings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListingImage",
                table: "Listings");
        }
    }
}
