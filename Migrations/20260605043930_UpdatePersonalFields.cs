using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiJBA.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePersonalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archivo",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "NroCuenta",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "dir_p",
                table: "personal");

            migrationBuilder.RenameColumn(
                name: "fs_p",
                table: "personal",
                newName: "fecha_voucher");

            migrationBuilder.RenameColumn(
                name: "fr_p",
                table: "personal",
                newName: "fecha_registro");

            migrationBuilder.AlterColumn<string>(
                name: "nombre_p",
                table: "personal",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "estado",
                table: "personal",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "correo_p",
                table: "personal",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cargo",
                table: "personal",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "ci_p",
                table: "personal",
                type: "varchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "direccion_p",
                table: "personal",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_salida",
                table: "personal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "foto_p",
                table: "personal",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tipo_preparacion",
                table: "personal",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ci_p",
                table: "operaciones",
                type: "varchar(16)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "direccion_p",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "fecha_salida",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "foto_p",
                table: "personal");

            migrationBuilder.DropColumn(
                name: "tipo_preparacion",
                table: "personal");

            migrationBuilder.RenameColumn(
                name: "fecha_voucher",
                table: "personal",
                newName: "fs_p");

            migrationBuilder.RenameColumn(
                name: "fecha_registro",
                table: "personal",
                newName: "fr_p");

            migrationBuilder.AlterColumn<string>(
                name: "nombre_p",
                table: "personal",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "estado",
                table: "personal",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "correo_p",
                table: "personal",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cargo",
                table: "personal",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "ci_p",
                table: "personal",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldMaxLength: 16);

            migrationBuilder.AddColumn<byte[]>(
                name: "Archivo",
                table: "personal",
                type: "image",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NroCuenta",
                table: "personal",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "dir_p",
                table: "personal",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ci_p",
                table: "operaciones",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldMaxLength: 20);
        }
    }
}
