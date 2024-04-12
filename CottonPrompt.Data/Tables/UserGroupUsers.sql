CREATE TABLE [dbo].[UserGroupUsers]
(
	[UserGroupId] INT NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedOn] DATETIME2 NOT NULL CONSTRAINT DF_UserGroupUsers_CreatedOn DEFAULT GETUTCDATE(),
	CONSTRAINT PK_UserGroupUsers_UserGroupId_UserId PRIMARY KEY ([UserGroupId], [UserId]),
    CONSTRAINT FK_UserGroupUsers_UserGroups FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[UserGroups]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_UserGroupUsers_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
)
