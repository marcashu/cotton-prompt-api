CREATE TABLE [dbo].[OrderDesignBrackets]
(
	[Id] INT CONSTRAINT PK_OrderDesignBracketId PRIMARY KEY IDENTITY(1,1),
	[Value] NVARCHAR(50) NOT NULL, 
    [SortOrder] INT NOT NULL,
	[Active] BIT NOT NULL CONSTRAINT DF_OrderDesignBrackets_Active DEFAULT 1
)
