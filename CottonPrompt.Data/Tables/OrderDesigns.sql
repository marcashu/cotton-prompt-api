CREATE TABLE [dbo].[OrderDesigns]
(
	[OrderId] INT NOT NULL,
	[LineId] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedOn] DATETIME2 NOT NULL CONSTRAINT DF_OrderDesignss_CreatedOn DEFAULT GETUTCDATE(),
	CONSTRAINT PK_OrderDesigns_OrderID_LineId PRIMARY KEY ([OrderId], [LineId]),
    CONSTRAINT FK_OrderDesigns_Orders FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id]) ON DELETE CASCADE
)
