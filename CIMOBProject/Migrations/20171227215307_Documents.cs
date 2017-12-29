using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CIMOBProject.Migrations
{
    public partial class Documents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Documents_DocumentId1",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_DocumentId1",
                table: "News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DocumentId1",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "Documents",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_News_DocumentId",
                table: "News",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Documents_DocumentId",
                table: "News",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Documents_DocumentId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_DocumentId",
                table: "News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "DocumentId1",
                table: "News",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "Documents",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Documents",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_News_DocumentId1",
                table: "News",
                column: "DocumentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Documents_DocumentId1",
                table: "News",
                column: "DocumentId1",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
