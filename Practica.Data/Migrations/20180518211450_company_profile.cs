using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Practica.Data.Migrations
{
    public partial class company_profile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishDate",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Activities",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateTable(
                name: "CompanyProfiles",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    Adress = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Logo = table.Column<byte[]>(type: "image", nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Website = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProfiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_CompanyProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyProfiles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishDate",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
