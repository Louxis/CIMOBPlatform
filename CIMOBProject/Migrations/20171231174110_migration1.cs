using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CIMOBProject.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_BilateralProtocols_BilateralProtocolId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_BilateralProtocolId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "BilateralProtocolId",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "BilateralProtocol1Id",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BilateralProtocol2Id",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BilateralProtocol3Id",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_BilateralProtocol1Id",
                table: "Applications",
                column: "BilateralProtocol1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_BilateralProtocol2Id",
                table: "Applications",
                column: "BilateralProtocol2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_BilateralProtocol3Id",
                table: "Applications",
                column: "BilateralProtocol3Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_BilateralProtocols_BilateralProtocol1Id",
                table: "Applications",
                column: "BilateralProtocol1Id",
                principalTable: "BilateralProtocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_BilateralProtocols_BilateralProtocol2Id",
                table: "Applications",
                column: "BilateralProtocol2Id",
                principalTable: "BilateralProtocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_BilateralProtocols_BilateralProtocol3Id",
                table: "Applications",
                column: "BilateralProtocol3Id",
                principalTable: "BilateralProtocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_BilateralProtocols_BilateralProtocol1Id",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_BilateralProtocols_BilateralProtocol2Id",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_BilateralProtocols_BilateralProtocol3Id",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_BilateralProtocol1Id",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_BilateralProtocol2Id",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_BilateralProtocol3Id",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "BilateralProtocol1Id",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "BilateralProtocol2Id",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "BilateralProtocol3Id",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "BilateralProtocolId",
                table: "Applications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_BilateralProtocolId",
                table: "Applications",
                column: "BilateralProtocolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_BilateralProtocols_BilateralProtocolId",
                table: "Applications",
                column: "BilateralProtocolId",
                principalTable: "BilateralProtocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
