CREATE TABLE [dbo].[Settings]
(
	[Id] INT CONSTRAINT PK_SettingId PRIMARY KEY IDENTITY(1,1),
	[QualityControlRate] DECIMAL(18, 2) NOT NULL CONSTRAINT DF_Settings_QualityControlRate DEFAULT 0.2,
	[ChangeRequestRate] DECIMAL(18, 2) NOT NULL CONSTRAINT DF_Settings_ChangeRequestRate DEFAULT 2,
	[ChangeRequestArtistsGroupId] INT NOT NULL,
	[TrainingGroupArtistsGroupId] INT NOT NULL,
	[TrainingGroupCheckersGroupId] INT NOT NULL,
	[UpdatedBy] UNIQUEIDENTIFIER NULL,
    [UpdatedOn] DATETIME2 NULL,
)
