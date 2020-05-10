using Microsoft.EntityFrameworkCore.Migrations;

namespace unibook.Migrations
{
    public partial class RemoveOwnedListings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Users_UserId1",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_UserId1",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "OwnedListings",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Listings");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Listings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_UserId",
                table: "Listings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Users_UserId",
                table: "Listings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Users_UserId",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Listings_UserId",
                table: "Listings");

            migrationBuilder.AddColumn<int>(
                name: "OwnedListings",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Listings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Listings",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_UserId1",
                table: "Listings",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Users_UserId1",
                table: "Listings",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
