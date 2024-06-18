CREATE TABLE [dbo].[OrderReports]
(
	[Id] INT CONSTRAINT PK_OrderReports_Id PRIMARY KEY IDENTITY(1,1),
	[OrderId] INT NOT NULL,
	[Reason] NVARCHAR(MAX) NOT NULL,
	[IsRedraw] BIT NOT NULL,
	[IsDesignSubmitted] BIT NOT NULL,
	[CheckerId] UNIQUEIDENTIFIER NULL,
	[ArtistStatus] NVARCHAR(50) NULL,
	[CheckerStatus] NVARCHAR(50) NULL,
	[CustomerStatus] NVARCHAR(50) NULL,
    [ReportedBy] UNIQUEIDENTIFIER NOT NULL,
	[ReportedOn] DATETIME2 NOT NULL CONSTRAINT DF_OrderReports_ReportedOn DEFAULT GETUTCDATE(),
    [ResolvedBy] UNIQUEIDENTIFIER NULL,
	[ResolvedOn] DATETIME2 NULL,
    CONSTRAINT FK_OrderReports_Orders FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_OrderReports_Reporters FOREIGN KEY ([ReportedBy]) REFERENCES [dbo].[Users]([Id]),
    CONSTRAINT FK_OrderReports_Resolvers FOREIGN KEY ([ResolvedBy]) REFERENCES [dbo].[Users]([Id]),
	CONSTRAINT FK_OrderReports_Checkers FOREIGN KEY ([CheckerId]) REFERENCES [dbo].[Users]([Id])
)
