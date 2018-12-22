-- This table stores the whole list of permissions
CREATE TABLE [dbo].[Permission]
(
    [Id] INT CONSTRAINT [PK_dbo_Permission] PRIMARY KEY,
    [Name] VARCHAR(32) NOT NULL CONSTRAINT [UK_dbo_Permission_Name] UNIQUE,
    [Description] NVARCHAR(128) NULL
);
GO
