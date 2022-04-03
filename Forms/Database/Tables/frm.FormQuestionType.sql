CREATE TABLE [frm].[FormQuestionType]
(
	[FormQuestionTypeId] INT IDENTITY(1001,1) NOT NULL,
	[FormTypeId] INT NOT NULL,
	[FormSectionTypeId] INT NOT NULL,
	[Code] VARCHAR(128) NOT NULL,
	[Text] VARCHAR(MAX) NOT NULL,
	[ResponseType] SMALLINT CONSTRAINT [DF_FormQuestionType_ResponseType] DEFAULT (0) NOT NULL,
	[ResponseList] VARCHAR(MAX) NOT NULL,
	[ResponseMaxLength] SMALLINT NULL,
	[IsRequired] BIT CONSTRAINT [DF_FormQuestionType_IsRequired] DEFAULT(1) NOT NULL,
	[ResponseValidationExpression] VARCHAR(1024) CONSTRAINT [DF_FormQuestionType_ResponseValidationExpression] DEFAULT ('') NOT NULL,
	[Hidden] BIT CONSTRAINT [DF_FormQuestionType] DEFAULT (0) NOT NULL,
	[Order] SMALLINT CONSTRAINT [DF_FormQuestionType_Order] DEFAULT (1) NOT NULL,
	[CreateTimestamp] DATETIME2(4) CONSTRAINT [DF_FormQuestionType_CreateTimestamp] DEFAULT(SYSUTCDATETIME()) NOT NULL,
	[UpdateTimestamp] DATETIME2(4) CONSTRAINT [DF_FormQuestionType_UdateTimestamp] DEFAULT(SYSUTCDATETIME()) NOT NULL,
	CONSTRAINT [FK_FormQuestionType] PRIMARY KEY CLUSTERED ([FormQuestionTypeId] DESC), 
    CONSTRAINT [FK_FormQuestionType_To_FormType] FOREIGN KEY ([FormTypeId]) REFERENCES [frm].[FormType]([FormTypeId]), 
    CONSTRAINT [FK_FormQuestionType_To_FormSectionType] FOREIGN KEY ([FormSectionTypeId]) REFERENCES [frm].[FormSectionType]([FormSectionTypeId])
)

GO

CREATE INDEX [IX_FormQuestionType_FormTypeId] ON [frm].[FormQuestionType] ([FormTypeId] DESC)

GO

CREATE INDEX [IX_FormQuestionType_FormSectionTypeId] ON [frm].[FormQuestionType] ([FormSectionTypeId] DESC)
