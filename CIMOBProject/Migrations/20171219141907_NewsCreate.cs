using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CIMOBProject.Migrations
{
    public partial class NewsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_ApplicationStat_ApplicationStatId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_AspNetUsers_EmployeeId1",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_AspNetUsers_StudentId1",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Application_ApplicationId",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationStat",
                table: "ApplicationStat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Application",
                table: "Application");

            migrationBuilder.RenameTable(
                name: "ApplicationStat",
                newName: "ApplicationStats");

            migrationBuilder.RenameTable(
                name: "Application",
                newName: "Applications");

            migrationBuilder.RenameIndex(
                name: "IX_Application_StudentId1",
                table: "Applications",
                newName: "IX_Applications_StudentId1");

            migrationBuilder.RenameIndex(
                name: "IX_Application_EmployeeId1",
                table: "Applications",
                newName: "IX_Applications_EmployeeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Application_ApplicationStatId",
                table: "Applications",
                newName: "IX_Applications_ApplicationStatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationStats",
                table: "ApplicationStats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationStats_ApplicationStatId",
                table: "Applications",
                column: "ApplicationStatId",
                principalTable: "ApplicationStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_EmployeeId1",
                table: "Applications",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_StudentId1",
                table: "Applications",
                column: "StudentId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Applications_ApplicationId",
                table: "Documents",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationStats_ApplicationStatId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_EmployeeId1",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_StudentId1",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Applications_ApplicationId",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationStats",
                table: "ApplicationStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.RenameTable(
                name: "ApplicationStats",
                newName: "ApplicationStat");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "Application");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_StudentId1",
                table: "Application",
                newName: "IX_Application_StudentId1");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_EmployeeId1",
                table: "Application",
                newName: "IX_Application_EmployeeId1");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_ApplicationStatId",
                table: "Application",
                newName: "IX_Application_ApplicationStatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationStat",
                table: "ApplicationStat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Application",
                table: "Application",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_ApplicationStat_ApplicationStatId",
                table: "Application",
                column: "ApplicationStatId",
                principalTable: "ApplicationStat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_AspNetUsers_EmployeeId1",
                table: "Application",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_AspNetUsers_StudentId1",
                table: "Application",
                column: "StudentId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Application_ApplicationId",
                table: "Documents",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
