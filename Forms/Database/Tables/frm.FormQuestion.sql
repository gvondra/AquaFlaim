CREATE TABLE [frm].[FormQuestion]
(
	[FormQuestionId] UNIQUEIDENTIFIER NOT NULL,
	[FormId] UNIQUEIDENTIFIER NOT NULL,
	[FormQuestionTypeId] INT NOT NULL,
	[Code] VARCHAR(128) NOT NULL,
	[Text] VARCHAR(MAX) NOT NULL,
	[Response] VARCHAR(MAX) NOT NULL,
	[CreateTimestamp] DATETIME2(4) CONSTRAINT [DF_FormQuestion_CreateTimestamp] DEFAULT(SYSUTCDATETIME()) NOT NULL,
	CONSTRAINT [PK_FormQuestion] PRIMARY KEY CLUSTERED ([FormQuestionId]), 
    CONSTRAINT [FK_FormQuestion_To_Form] FOREIGN KEY ([FormId]) REFERENCES [frm].[Form]([FormId]), 
    CONSTRAINT [FK_FormQuestion_To_FormQuestionType] FOREIGN KEY ([FormQuestionTypeId]) REFERENCES [frm].[FormQuestionType]([FormQuestionTypeId])
)

GO

CREATE INDEX [IX_FormQuestion_FormId] ON [frm].[FormQuestion] ([FormId])

GO

CREATE INDEX [IX_FormQuestion_FormQuestionTypeId] ON [frm].[FormQuestion] ([FormQuestionTypeId] DESC)
