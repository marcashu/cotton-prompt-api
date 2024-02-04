CREATE TABLE [dbo].[OrderDesignComments]
(
	[Id] INT CONSTRAINT PK_OrderDesignComments_Id PRIMARY KEY IDENTITY(1,1),
	[OrderDesignId] INT NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[Comment] NVARCHAR(MAX) NOT NULL,
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedOn] DATETIME2 NOT NULL CONSTRAINT DF_OrderDesignComments_CreatedOn DEFAULT GETUTCDATE(),
    CONSTRAINT FK_OrderDesignComments_OrderDesigns FOREIGN KEY ([OrderDesignId]) REFERENCES [dbo].[OrderDesigns]([Id]) ON DELETE CASCADE,
)
