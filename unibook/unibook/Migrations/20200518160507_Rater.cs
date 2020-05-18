using Microsoft.EntityFrameworkCore.Migrations;

namespace unibook.Migrations
{
    public partial class Rater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RaterId",
                table: "Ratings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RaterId",
                table: "Ratings",
                column: "RaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_RaterId",
                table: "Ratings",
                column: "RaterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_RaterId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RaterId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "RaterId",
                table: "Ratings");
        }
    }
}
