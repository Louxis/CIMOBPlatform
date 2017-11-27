using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CIMOBProject.Data.Migrations
{
    public partial class CreateCollegeAndSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollegeID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "College",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollegeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_College", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollegeSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollegeId = table.Column<int>(type: "int", nullable: true),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollegeSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollegeSubject_College_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "College",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CollegeID",
                table: "Students",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_CollegeSubject_CollegeId",
                table: "CollegeSubject",
                column: "CollegeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_College_CollegeID",
                table: "Students",
                column: "CollegeID",
                principalTable: "College",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_College_CollegeID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "CollegeSubject");

            migrationBuilder.DropTable(
                name: "College");

            migrationBuilder.DropIndex(
                name: "IX_Students_CollegeID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CollegeID",
                table: "Students");
        }
    }
}
