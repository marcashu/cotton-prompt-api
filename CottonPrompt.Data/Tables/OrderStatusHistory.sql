CREATE TABLE [dbo].[OrderStatusHistory]
(
	[Id] INT CONSTRAINT PK_OrderStatusHistory_Id PRIMARY KEY IDENTITY(1,1),
	[OrderId] INT NOT NULL,
	[Status] NVARCHAR(50) NOT NULL,
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedOn] DATETIME2 NOT NULL CONSTRAINT DF_OrderStatusHistory_CreatedOn DEFAULT GETUTCDATE(),
    CONSTRAINT FK_OrderStatusHistory_Orders FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id]) ON DELETE CASCADE
)
