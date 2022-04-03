CREATE TABLE [frm].[Form]
(
	[FormId] UNIQUEIDENTIFIER NOT NULL,
	[FormTypeId] INT NOT NULL,
	[CreateUserId] UNIQUEIDENTIFIER NOT NULL,
	[CreateTimestamp] DATETIME2(4) CONSTRAINT [DF_Form_CreateTimestamp] DEFAULT(SYSUTCDATETIME()) NOT NULL,
	CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED (FormId), 
    CONSTRAINT [FK_Form_To_FormType] FOREIGN KEY ([FormTypeId]) REFERENCES [frm].[FormType]([FormTypeId])
)

GO

CREATE INDEX [IX_Form_FormTypeId] ON [frm].[Form] ([FormTypeId] DESC)
