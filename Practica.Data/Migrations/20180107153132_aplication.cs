using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Practica.Data.Migrations
{
    public partial class aplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Aplications",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Aplications_ActivityId",
                table: "Aplications",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Aplications_UserId",
                table: "Aplications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aplications_Activities_ActivityId",
                table: "Aplications",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aplications_AspNetUsers_UserId",
                table: "Aplications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aplications_Activities_ActivityId",
                table: "Aplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Aplications_AspNetUsers_UserId",
                table: "Aplications");

            migrationBuilder.DropIndex(
                name: "IX_Aplications_ActivityId",
                table: "Aplications");

            migrationBuilder.DropIndex(
                name: "IX_Aplications_UserId",
                table: "Aplications");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Aplications",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);
        }
    }
}
