using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Practica.Data.Migrations
{
    public partial class CompanyProfileLogoExtenesion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoExtension",
                table: "CompanyProfiles",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoExtension",
                table: "CompanyProfiles");
        }
    }
}
