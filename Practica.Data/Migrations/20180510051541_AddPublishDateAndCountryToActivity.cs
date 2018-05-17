using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Practica.Data.Migrations
{
    public partial class AddPublishDateAndCountryToActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AplicationEndDate",
                table: "Activities",
                newName: "PublishDate");

            migrationBuilder.RenameColumn(
                name: "Addres",
                table: "Activities",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Activities",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Activities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Activities",
                newName: "AplicationEndDate");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Activities",
                newName: "Addres");
        }
    }
}
