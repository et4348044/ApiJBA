-- =========================================================================
-- SCRIPT DE INICIALIZACIÓN DE BASE DE DATOS: BD_JBA
-- Generado para el sistema de control de acceso ApiJBA.
-- Diseñado con enfoque en alto rendimiento y optimización.
-- =========================================================================

-- Opcional: Crear la base de datos si no existe
-- CREATE DATABASE [BD_JBA];
-- GO
-- USE [BD_JBA];
-- GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [personal] (
    [ci_p] nvarchar(20) NOT NULL,
    [nombre_p] nvarchar(50) NOT NULL,
    [nivel] int NOT NULL,
    [estado] int NOT NULL,
    [cargo] nvarchar(25) NOT NULL,
    [fr_p] datetime2 NOT NULL,
    [fs_p] datetime2 NULL,
    [dir_p] nvarchar(120) NOT NULL,
    [correo_p] nvarchar(50) NULL,
    [NroCuenta] nvarchar(255) NOT NULL,
    [Archivo] image NULL,
    CONSTRAINT [PK_personal] PRIMARY KEY ([ci_p])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260520032153_InitialMigration', N'8.0.10');
GO

COMMIT;
GO
