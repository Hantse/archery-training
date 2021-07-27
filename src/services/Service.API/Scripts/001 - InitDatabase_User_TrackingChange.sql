USE [master]
GO
DROP DATABASE [ARCHERY_TRAINING]
GO
CREATE DATABASE [ARCHERY_TRAINING]
GO
USE [ARCHERY_TRAINING]

IF USER_ID('TrainingArcheryUser') IS NULL BEGIN
    CREATE LOGIN TrainingArcheryUser WITH PASSWORD = 'CdH7FNWVtHnf';
    CREATE USER TrainingArcheryUser FOR LOGIN TrainingArcheryUser;  
END
GO

IF SCHEMA_ID('TrainingArcheryApi') IS NULL BEGIN
    EXECUTE('Create schema TrainingArcheryApi')
END
GO

GRANT EXECUTE ON SCHEMA::TrainingArcheryApi to TrainingArcheryUser
GO

IF NOT EXISTS(SELECT * FROM sys.sequences WHERE [name] = 'Ids')
BEGIN
    CREATE SEQUENCE dbo.Ids
    AS INT
    START WITH 1;
END
GO

DROP TABLE IF EXISTS dbo.[User];
CREATE TABLE dbo.[User]
(
    [Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL DEFAULT(NEWID()),
    UserId NVARCHAR(MAX) NOT NULL,
    CreateAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    CreateBy NVARCHAR(48) NOT NULL,
    DeleteAt DATETIME NULL,
    DeleteBy NVARCHAR(48) NULL,
    UpdateAt DATETIME NULL,
    UpdateBy NVARCHAR(48) NULL
);
GO

DROP TABLE IF EXISTS dbo.TrainingSession;
CREATE TABLE dbo.TrainingSession
(
    [Id] INT PRIMARY KEY NOT NULL DEFAULT(NEXT VALUE for dbo.Ids),
    [Date] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    Duration BIGINT NOT NULL DEFAULT(0),
    [Status] NVARCHAR(120) NOT NULL DEFAULT('IN_PROGRESS'),
    [Type] NVARCHAR(120) NOT NULL,
    [Performance] NVARCHAR(120) NOT NULL,
    [Set] INT NULL,
    [Total] INT NULL,
    [TotalArrow] INT NULL,
    CreateAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    CreateBy NVARCHAR(48) NOT NULL,
    DeleteAt DATETIME NULL,
    DeleteBy NVARCHAR(48) NULL,
    UpdateAt DATETIME NULL,
    UpdateBy NVARCHAR(48) NULL
);
GO

DROP TABLE IF EXISTS dbo.TrainingSessionSet;
CREATE TABLE dbo.TrainingSessionSet
(
    [Id] INT PRIMARY KEY NOT NULL DEFAULT(NEXT VALUE for dbo.Ids),
    [TrainingSessionId] INT NOT NULL,
    [Order] INT NOT NULL,
    [Value] INT NOT NULL,
    CreateAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    CreateBy NVARCHAR(48) NOT NULL,
    DeleteAt DATETIME NULL,
    DeleteBy NVARCHAR(48) NULL,
    UpdateAt DATETIME NULL,
    UpdateBy NVARCHAR(48) NULL
);
GO

IF NOT EXISTS(SELECT * FROM sys.change_tracking_databases WHERE database_id = db_id())
BEGIN
    ALTER DATABASE ARCHERY_TRAINING 
    SET CHANGE_TRACKING = ON
    (CHANGE_RETENTION = 60 DAYS, AUTO_CLEANUP = on)
END
GO

IF NOT EXISTS(SELECT * FROM sys.change_tracking_tables WHERE [object_id]=object_id('dbo.TrainingSession'))
BEGIN
    ALTER TABLE dbo.TrainingSession
    ENABLE CHANGE_TRACKING
END
GO

IF NOT EXISTS(SELECT * FROM sys.change_tracking_tables WHERE [object_id]=object_id('dbo.TrainingSessionSet'))
BEGIN
    ALTER TABLE dbo.TrainingSessionSet
    ENABLE CHANGE_TRACKING
END
GO