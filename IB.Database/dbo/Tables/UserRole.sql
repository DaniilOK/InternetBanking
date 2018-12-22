CREATE TABLE [dbo].[UserRole]
(
	[UserId] UNIQUEIDENTIFIER NOT NULL, 
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT [PK_dbo_RoleUser] PRIMARY KEY ([RoleId], [UserId]),
	CONSTRAINT [FK_dbo_RoleUser_UserId_User_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo_RoleUser_RoleId_Role_Id] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE
)
