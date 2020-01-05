using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class DivineDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceEngineer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    DateOfJoin = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEngineer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyAddress = table.Column<string>(nullable: true),
                    CompanyPhone = table.Column<string>(nullable: true),
                    CompanyWebSite = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    IsDelete = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceQuotation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    IsReply = table.Column<string>(nullable: true),
                    ServiceTitle = table.Column<string>(nullable: true),
                    ServiceDescription = table.Column<string>(nullable: true),
                    ServiceEngId = table.Column<Guid>(nullable: true),
                    ServiceDate = table.Column<DateTime>(nullable: true),
                    QuotationTitle = table.Column<string>(nullable: true),
                    QuotationDesc = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceQuotation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceQuotation_ServiceEngineer_ServiceEngId",
                        column: x => x.ServiceEngId,
                        principalTable: "ServiceEngineer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceQuotation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UploadDocumentImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    DocumentPath = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    IsDelete = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadDocumentImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadDocumentImage_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false),
                    ServiceAttachPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAttachment_ServiceQuotation_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceQuotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAttachment_ServiceId",
                table: "ServiceAttachment",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceQuotation_ServiceEngId",
                table: "ServiceQuotation",
                column: "ServiceEngId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceQuotation_UserId",
                table: "ServiceQuotation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadDocumentImage_UserId",
                table: "UploadDocumentImage",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceAttachment");

            migrationBuilder.DropTable(
                name: "UploadDocumentImage");

            migrationBuilder.DropTable(
                name: "ServiceQuotation");

            migrationBuilder.DropTable(
                name: "ServiceEngineer");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
