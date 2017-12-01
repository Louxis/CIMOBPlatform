using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CIMOBProject.Migrations
{
    public partial class Subjectid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollegeSubjectId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CollegeSubjectId",
                table: "AspNetUsers",
                column: "CollegeSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CollegeSubjects_CollegeSubjectId",
                table: "AspNetUsers",
                column: "CollegeSubjectId",
                principalTable: "CollegeSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CollegeSubjects_CollegeSubjectId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CollegeSubjectId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CollegeSubjectId",
                table: "AspNetUsers");
        }
    }
}
