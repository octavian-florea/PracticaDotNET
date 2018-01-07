using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Practica.Data.Migrations
{
    public partial class rename_col_activity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "UnpublishDate",
                table: "Activities",
                newName: "AplicationEndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AplicationEndDate",
                table: "Activities",
                newName: "UnpublishDate");

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Activities",
                nullable: false,
                defaultValue: false);
        }
    }
}
