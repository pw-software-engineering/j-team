using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HotelReservationSystem.Infrastructure.Migrations
{
    public partial class HotelFileToFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelFiles");

            migrationBuilder.DropTable(
                name: "OfferFiles");

            migrationBuilder.DropTable(
                name: "PreviewHotelFiles");

            migrationBuilder.DropTable(
                name: "PreviewOfferFiles");

            migrationBuilder.AddColumn<int>(
                name: "OfferPreviewPictureId",
                table: "Offers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelPreviewPictureId",
                table: "Hotels",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", maxLength: 20000, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    HotelId = table.Column<int>(type: "integer", nullable: true),
                    OfferId = table.Column<int>(type: "integer", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OfferPreviewPictureId",
                table: "Offers",
                column: "OfferPreviewPictureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelPreviewPictureId",
                table: "Hotels",
                column: "HotelPreviewPictureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_HotelId",
                table: "Files",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_OfferId",
                table: "Files",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Files_HotelPreviewPictureId",
                table: "Hotels",
                column: "HotelPreviewPictureId",
                principalTable: "Files",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Files_OfferPreviewPictureId",
                table: "Offers",
                column: "OfferPreviewPictureId",
                principalTable: "Files",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Files_HotelPreviewPictureId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Files_OfferPreviewPictureId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Offers_OfferPreviewPictureId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelPreviewPictureId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "OfferPreviewPictureId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "HotelPreviewPictureId",
                table: "Hotels");

            migrationBuilder.CreateTable(
                name: "HotelFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", maxLength: 20000, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFiles", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_HotelFiles_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", maxLength: 20000, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OfferId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferFiles", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_OfferFiles_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreviewHotelFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", maxLength: 20000, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviewHotelFiles", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_PreviewHotelFiles_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreviewOfferFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", maxLength: 20000, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OfferId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviewOfferFiles", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_PreviewOfferFiles_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelFiles_HotelId",
                table: "HotelFiles",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferFiles_OfferId",
                table: "OfferFiles",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_PreviewHotelFiles_HotelId",
                table: "PreviewHotelFiles",
                column: "HotelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreviewOfferFiles_OfferId",
                table: "PreviewOfferFiles",
                column: "OfferId",
                unique: true);
        }
    }
}
