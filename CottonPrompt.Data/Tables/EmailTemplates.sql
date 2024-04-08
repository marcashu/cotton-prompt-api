CREATE TABLE [dbo].[EmailTemplates]
(
	[Id] INT CONSTRAINT PK_EmailTemplateId PRIMARY KEY IDENTITY(1,1),
	[OrderProofReadyEmail] NVARCHAR(MAX) NOT NULL,
	[UpdatedBy] UNIQUEIDENTIFIER NULL,
    [UpdatedOn] DATETIME2 NULL,
)
