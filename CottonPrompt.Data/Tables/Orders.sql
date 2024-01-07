CREATE TABLE [dbo].[Orders]
(
	[Id] INT CONSTRAINT PK_OrderId PRIMARY KEY IDENTITY(1,1),
	[OrderNumber] NVARCHAR(50) NOT NULL,
	[Priority] BIT NOT NULL,
	[Concept] NVARCHAR(MAX) NOT NULL,
	[PrintColorId] INT NOT NULL CONSTRAINT FK_Orders_OrderPrintColors REFERENCES [dbo].[OrderPrintColors]([Id]),
	[DesignBracketId] INT NOT NULL CONSTRAINT FK_Orders_OrderDesignBrackets REFERENCES [dbo].[OrderDesignBrackets]([Id]),
	[OutputSizeId] INT NOT NULL CONSTRAINT FK_Orders_OrderOutputSizes REFERENCES [dbo].[OrderOutputSizes]([Id]), 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedOn] DATETIME2 NOT NULL CONSTRAINT DF_Orders_CreatedOn DEFAULT GETUTCDATE(),
	[ArtistId] UNIQUEIDENTIFIER NULL,
	[CheckerId] UNIQUEIDENTIFIER NULL,
	[ArtistStatus] NVARCHAR(50) NULL,
	[CheckerStatus] NVARCHAR(50) NULL,
    [UpdatedBy] UNIQUEIDENTIFIER NULL, 
    [UpdatedOn] DATETIME2 NULL
)
