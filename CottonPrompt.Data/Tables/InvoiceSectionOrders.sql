CREATE TABLE [dbo].[InvoiceSectionOrders]
(
	[Id] INT CONSTRAINT PK_InvoiceSectionOrderId PRIMARY KEY IDENTITY(1,1),
	[InvoiceSectionId] INT NOT NULL,
	[OrderId] INT NOT NULL,
	CONSTRAINT FK_InvoiceSectionOrders_InvoiceSections FOREIGN KEY ([InvoiceSectionId]) REFERENCES [dbo].[InvoiceSections]([Id]) ON DELETE CASCADE,
	CONSTRAINT FK_InvoiceSectionOrders_Orders FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([Id])
)
