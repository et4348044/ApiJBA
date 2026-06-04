using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiJBA.Migrations
{
    /// <inheritdoc />
    public partial class AddOperacionesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "operaciones",
                columns: table => new
                {
                    id_operacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ci_p = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    registro_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tipo_operacion = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operaciones", x => x.id_operacion);
                    table.ForeignKey(
                        name: "FK_operaciones_personal_ci_p",
                        column: x => x.ci_p,
                        principalTable: "personal",
                        principalColumn: "ci_p",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_operaciones_ci_p",
                table: "operaciones",
                column: "ci_p");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operaciones");
        }
    }
}
