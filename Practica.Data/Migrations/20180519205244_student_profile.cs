using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Practica.Data.Migrations
{
    public partial class student_profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsProfile",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),  
                    Description = table.Column<string>(type: "text", nullable: true),
                    FacultyId = table.Column<int>(maxLength: 10, nullable: false),     
                    Specialization = table.Column<string>(maxLength: 100, nullable: false),
                    StudyYear = table.Column<int>(nullable: false),
                    Telephone = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    CV = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_StudentsProfile_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsProfile_FacultyId",
                table: "StudentsProfile",
                column: "FacultyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsProfile");
        }
    }
}
