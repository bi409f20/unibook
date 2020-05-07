using Microsoft.EntityFrameworkCore.Migrations;

namespace unibook.Migrations
{
    public partial class BookListings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Listings_ListingId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ListingId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "BookISBN",
                table: "Listings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_BookISBN",
                table: "Listings",
                column: "BookISBN");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Books_BookISBN",
                table: "Listings",
                column: "BookISBN",
                principalTable: "Books",
                principalColumn: "ISBN",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Books_BookISBN",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_BookISBN",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "BookISBN",
                table: "Listings");

            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ListingId",
                table: "Books",
                column: "ListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Listings_ListingId",
                table: "Books",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
