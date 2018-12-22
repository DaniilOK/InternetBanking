CREATE TABLE [dbo].[UserPermission]
(
    [UserId] UNIQUEIDENTIFIER
        CONSTRAINT [FK_dbo_UserPermission_dbo_User] FOREIGN KEY
        REFERENCES [dbo].[User]([Id]) ON DELETE CASCADE,
    [PermissionId] INT
        CONSTRAINT [FK_dbo_UserPermission_dbo_Permission] FOREIGN KEY
        REFERENCES [dbo].[Permission]([Id]) ON DELETE CASCADE,
    CONSTRAINT [PK_dbo_UserPermission] PRIMARY KEY ([UserId], [PermissionId])
);
GO

CREATE INDEX [IX_dbo_UserPermission_UserId] ON [dbo].[UserPermission]([UserId]);
GO

CREATE INDEX [IX_dbo_UserPermission_PermissionId] ON [dbo].[UserPermission]([PermissionId]);
GO
