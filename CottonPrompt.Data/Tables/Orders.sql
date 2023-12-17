CREATE TABLE [dbo].[Orders]
(
	[Id] INT CONSTRAINT PK_OrderId PRIMARY KEY IDENTITY(1,1),
	[Number] NVARCHAR(50) NOT NULL,
	[IsPriority] BIT NOT NULL,
	[Concept] NVARCHAR(MAX) NOT NULL,
	[PrintColor] NVARCHAR(50) NOT NULL,
	[DesignBracketId] INT NOT NULL CONSTRAINT FK_Orders_OrderDesignBrackets REFERENCES [dbo].[OrderDesignBrackets]([Id]), 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
	[CreatedOn] DATETIME2 NOT NULL CONSTRAINT DF_Orders_CreatedOn DEFAULT GETUTCDATE(),
)
