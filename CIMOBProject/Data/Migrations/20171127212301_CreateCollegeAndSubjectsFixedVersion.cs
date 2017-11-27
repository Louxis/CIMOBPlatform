using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CIMOBProject.Data.Migrations
{
    public partial class CreateCollegeAndSubjectsFixedVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollegeSubject_College_CollegeId",
                table: "CollegeSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_College_CollegeID",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollegeSubject",
                table: "CollegeSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_College",
                table: "College");

            migrationBuilder.RenameTable(
                name: "CollegeSubject",
                newName: "CollgeSubjects");

            migrationBuilder.RenameTable(
                name: "College",
                newName: "Colleges");

            migrationBuilder.RenameIndex(
                name: "IX_CollegeSubject_CollegeId",
                table: "CollgeSubjects",
                newName: "IX_CollgeSubjects_CollegeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollgeSubjects",
                table: "CollgeSubjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colleges",
                table: "Colleges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollgeSubjects_Colleges_CollegeId",
                table: "CollgeSubjects",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Colleges_CollegeID",
                table: "Students",
                column: "CollegeID",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollgeSubjects_Colleges_CollegeId",
                table: "CollgeSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Colleges_CollegeID",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollgeSubjects",
                table: "CollgeSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colleges",
                table: "Colleges");

            migrationBuilder.RenameTable(
                name: "CollgeSubjects",
                newName: "CollegeSubject");

            migrationBuilder.RenameTable(
                name: "Colleges",
                newName: "College");

            migrationBuilder.RenameIndex(
                name: "IX_CollgeSubjects_CollegeId",
                table: "CollegeSubject",
                newName: "IX_CollegeSubject_CollegeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollegeSubject",
                table: "CollegeSubject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_College",
                table: "College",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollegeSubject_College_CollegeId",
                table: "CollegeSubject",
                column: "CollegeId",
                principalTable: "College",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_College_CollegeID",
                table: "Students",
                column: "CollegeID",
                principalTable: "College",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
