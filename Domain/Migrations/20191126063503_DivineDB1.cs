using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class DivineDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadDocumentImage_Users_UserId",
                table: "UploadDocumentImage");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UploadDocumentImage",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_UploadDocumentImage_Users_UserId",
                table: "UploadDocumentImage",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadDocumentImage_Users_UserId",
                table: "UploadDocumentImage");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UploadDocumentImage",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UploadDocumentImage_Users_UserId",
                table: "UploadDocumentImage",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
