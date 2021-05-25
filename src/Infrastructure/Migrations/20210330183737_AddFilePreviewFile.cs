using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HotelReservationSystem.Infrastructure.Migrations
{
    public partial class AddFilePreviewFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferPreviewPicture",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "HotelPreviewPicture",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "Hotels");

            migrationBuilder.AddColumn<int>(
                name: "OfferPreviewPictureId",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", maxLength: 20000, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_Files_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreviewFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", maxLength: 20000, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviewFiles", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_PreviewFiles_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Offers_OfferPreviewPictureId",
                table: "Offers",
                column: "OfferPreviewPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_FileOffer_PicturesFileId",
                table: "FileOffer",
                column: "PicturesFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_HotelId",
                table: "Files",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_PreviewFiles_HotelId",
                table: "PreviewFiles",
                column: "HotelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_PreviewFiles_OfferPreviewPictureId",
                table: "Offers",
                column: "OfferPreviewPictureId",
                principalTable: "PreviewFiles",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_PreviewFiles_OfferPreviewPictureId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "FileOffer");

            migrationBuilder.DropTable(
                name: "PreviewFiles");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Offers_OfferPreviewPictureId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OfferPreviewPictureId",
                table: "Offers");

            migrationBuilder.AddColumn<byte[]>(
                name: "OfferPreviewPicture",
                table: "Offers",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[][]>(
                name: "Pictures",
                table: "Offers",
                type: "bytea[]",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "HotelPreviewPicture",
                table: "Hotels",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[][]>(
                name: "Pictures",
                table: "Hotels",
                type: "bytea[]",
                nullable: true);
        }
    }
}
