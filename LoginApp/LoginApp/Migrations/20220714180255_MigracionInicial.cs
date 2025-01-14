﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginApp.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuentasCorreo",
                columns: table => new
                {
                    Email = table.Column<string>(unicode: false, nullable: false),
                    AccesToken = table.Column<string>(unicode: false, nullable: true),
                    RefreshToken = table.Column<string>(unicode: false, nullable: true),
                    Scope = table.Column<string>(unicode: false, nullable: true),
                    ExpiracionAccesToken = table.Column<DateTime>(nullable: false),
                    ExpiracionRefreshToken = table.Column<DateTime>(nullable: false),
                    ExcepcionAutenticacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasCorreo", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuentasCorreo");
        }
    }
}
