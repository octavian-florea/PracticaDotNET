using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Practica.Data.Migrations
{
    public partial class update_aplication_entety : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Aplications",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Aplications",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "Aplications",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "StudyYear",
                table: "Aplications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StundetMessage",
                table: "Aplications",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aplications_FacultyId",
                table: "Aplications",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aplications_Faculties_FacultyId",
                table: "Aplications",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aplications_Faculties_FacultyId",
                table: "Aplications");

            migrationBuilder.DropIndex(
                name: "IX_Aplications_FacultyId",
                table: "Aplications");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Aplications");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Aplications");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Aplications");

            migrationBuilder.DropColumn(
                name: "StudyYear",
                table: "Aplications");

            migrationBuilder.DropColumn(
                name: "StundetMessage",
                table: "Aplications");
        }
    }
}
