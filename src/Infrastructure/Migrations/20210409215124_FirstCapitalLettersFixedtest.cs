using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationSystem.Infrastructure.Migrations
{
    public partial class FirstCapitalLettersFixedtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Reviews",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Reviews",
                newName: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Reviews",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Reviews",
                newName: "content");
        }
    }
}
