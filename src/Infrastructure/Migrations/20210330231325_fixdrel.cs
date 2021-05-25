using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationSystem.Infrastructure.Migrations
{
    public partial class fixdrel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Hotels_HotelId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_PreviewFiles_OfferPreviewPictureFileId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_PreviewFiles_Hotels_HotelId",
                table: "PreviewFiles");

            migrationBuilder.DropTable(
                name: "FileOffer");

            migrationBuilder.DropIndex(
                name: "IX_Offers_OfferPreviewPictureFileId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OfferPreviewPictureFileId",
                table: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "PreviewFiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "PreviewFiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Files",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Files",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreviewFiles_OfferId",
                table: "PreviewFiles",
                column: "OfferId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_OfferId",
                table: "Files",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Hotels_HotelId",
                table: "Files",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Offers_OfferId",
                table: "Files",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreviewFiles_Hotels_HotelId",
                table: "PreviewFiles",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreviewFiles_Offers_OfferId",
                table: "PreviewFiles",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Hotels_HotelId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Offers_OfferId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_PreviewFiles_Hotels_HotelId",
                table: "PreviewFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PreviewFiles_Offers_OfferId",
                table: "PreviewFiles");

            migrationBuilder.DropIndex(
                name: "IX_PreviewFiles_OfferId",
                table: "PreviewFiles");

            migrationBuilder.DropIndex(
                name: "IX_Files_OfferId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "PreviewFiles");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "PreviewFiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfferPreviewPictureFileId",
                table: "Offers",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Files",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FileOffer",
                columns: table => new
                {
                    OffersOfferId = table.Column<int>(type: "integer", nullable: false),
                    PicturesFileId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileOffer", x => new { x.OffersOfferId, x.PicturesFileId });
                    table.ForeignKey(
                        name: "FK_FileOffer_Files_PicturesFileId",
                        column: x => x.PicturesFileId,
                        principalTable: "Files",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileOffer_Offers_OffersOfferId",
                        column: x => x.OffersOfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OfferPreviewPictureFileId",
                table: "Offers",
                column: "OfferPreviewPictureFileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileOffer_PicturesFileId",
                table: "FileOffer",
                column: "PicturesFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Hotels_HotelId",
                table: "Files",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_PreviewFiles_OfferPreviewPictureFileId",
                table: "Offers",
                column: "OfferPreviewPictureFileId",
                principalTable: "PreviewFiles",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreviewFiles_Hotels_HotelId",
                table: "PreviewFiles",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
