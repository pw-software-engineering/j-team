using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationSystem.Infrastructure.Migrations
{
    public partial class HotelPasswordAndToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Hotels",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Hotels",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Hotels");
        }
    }
}
