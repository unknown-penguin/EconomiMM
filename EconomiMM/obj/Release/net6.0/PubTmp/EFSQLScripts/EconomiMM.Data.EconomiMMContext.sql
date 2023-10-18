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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230804083640_AddMaterialTypes')
BEGIN
    CREATE TABLE [Material] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Count] int NOT NULL,
        [Price] int NOT NULL,
        [Reserved] int NOT NULL,
        [Thickness] real NOT NULL,
        CONSTRAINT [PK_Material] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230804083640_AddMaterialTypes')
BEGIN
    CREATE TABLE [MaterialType] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Manufacturer] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MaterialType] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230804083640_AddMaterialTypes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230804083640_AddMaterialTypes', N'6.0.20');
END;
GO

COMMIT;
GO

