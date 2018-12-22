CREATE TABLE [dbo].[User]
(
    [Id] UNIQUEIDENTIFIER CONSTRAINT [PK_dbo_User] PRIMARY KEY,
    [Login] NVARCHAR(128) NOT NULL CONSTRAINT [UK_dbo_User_Login] UNIQUE,
    [FirstName] NVARCHAR(128) NOT NULL,
    [LastName] NVARCHAR(128) NOT NULL,
    [Inactive] BIT NOT NULL,
    [Email] NVARCHAR(128) NOT NULL CONSTRAINT [UK_dbo_User_Email] UNIQUE,
	[IsEmailConfirmed] BIT NOT NULL,
	[PasswordHash] NVARCHAR(128) NOT NULL,
    [PasswordSalt] NVARCHAR(128) NOT NULL
);
GO

CREATE INDEX [IX_dbo_User_FirstName_LastName] ON [dbo].[User]([FirstName], [LastName]);
GO
