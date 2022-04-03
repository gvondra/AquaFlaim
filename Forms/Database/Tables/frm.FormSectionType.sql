CREATE TABLE [frm].[FormSectionType]
(
	[FormSectionTypeId] INT IDENTITY(1001,1) NOT NULL,
	[FormTypeId] INT NOT NULL,
	[Title] VARCHAR(256) NOT NULL,
	[Order] SMALLINT CONSTRAINT [DF_FormSectionType_Order] DEFAULT (1) NOT NULL,
	[Hidden] BIT CONSTRAINT [DF_FormSectionType] DEFAULT (0) NOT NULL,
	[CreateTimestamp] DATETIME2(4) CONSTRAINT [DF_FormSectionType_CreateTimestamp] DEFAULT(SYSUTCDATETIME()) NOT NULL,
	[UpdateTimestamp] DATETIME2(4) CONSTRAINT [DF_FormSectionType_UdateTimestamp] DEFAULT(SYSUTCDATETIME()) NOT NULL,
	CONSTRAINT [PK_FormSectionType] PRIMARY KEY CLUSTERED ([FormSectionTypeId] DESC), 
    CONSTRAINT [FK_FormSectionType_To_FormType] FOREIGN KEY ([FormTypeId]) REFERENCES [frm].[FormType]([FormTypeId])
)

GO

CREATE INDEX [IX_FormSectionType_FormTypeId] ON [frm].[FormSectionType] ([FormTypeId] DESC)
