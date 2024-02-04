CREATE TABLE [dbo].[OrderImageReferences]
(
	[OrderId] INT NOT NULL,
	[LineId] INT NOT NULL,
	[Url] NVARCHAR(MAX) NOT NULL,
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedOn] DATETIME2 NOT NULL CONSTRAINT DF_OrderImageReferences_CreatedOn DEFAULT GETUTCDATE()
	CONSTRAINT PK_OrderImageReferences_OrderID_LineId PRIMARY KEY ([OrderId], [LineId]),
    CONSTRAINT FK_OrderImageReferences_Orders FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id]) ON DELETE CASCADE
)
