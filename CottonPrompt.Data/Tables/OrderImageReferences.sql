CREATE TABLE [dbo].[OrderImageReferences]
(
	[OrderId] INT NOT NULL,
	[LineId] INT NOT NULL,
	[Url] NVARCHAR(MAX) NOT NULL
	CONSTRAINT PK_OrderImageReferences_OrderID_LineId PRIMARY KEY ([OrderId], [LineId]),
    CONSTRAINT FK_OrderImageReferences_Orders FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id])
)
