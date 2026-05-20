using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiJBA.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "personal",
                columns: table => new
                {
                    ci_p = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nombre_p = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nivel = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    cargo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    fr_p = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fs_p = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dir_p = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    correo_p = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NroCuenta = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Archivo = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personal", x => x.ci_p);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "personal");
        }
    }
}
