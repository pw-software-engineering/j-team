using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HotelReservationSystem.Infrastructure.Migrations
{
    public partial class AddReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_PreviewFiles_OfferPreviewPictureId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_OfferPreviewPictureId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OfferPreviewPictureId",
                table: "Offers");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PreviewFiles",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Offers",
                type: "boolean",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferPreviewPictureFileId",
                table: "Offers",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Files",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    OfferId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OfferPreviewPictureFileId",
                table: "Offers",
                column: "OfferPreviewPictureFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientId",
                table: "Reviews",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OfferId",
                table: "Reviews",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_PreviewFiles_OfferPreviewPictureFileId",
                table: "Offers",
                column: "OfferPreviewPictureFileId",
                principalTable: "PreviewFiles",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_PreviewFiles_OfferPreviewPictureFileId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Offers_OfferPreviewPictureFileId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OfferPreviewPictureFileId",
                table: "Offers");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PreviewFiles",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Offers",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferPreviewPictureId",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Files",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OfferPreviewPictureId",
                table: "Offers",
                column: "OfferPreviewPictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_PreviewFiles_OfferPreviewPictureId",
                table: "Offers",
                column: "OfferPreviewPictureId",
                principalTable: "PreviewFiles",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
