CREATE TABLE [lg].[Exception]
(
	[ExceptionId] UNIQUEIDENTIFIER NOT NULL,
	[ParentExceptionId] UNIQUEIDENTIFIER NULL,
	[Message] NVARCHAR(2000) NOT NULL,
	[TypeName] NVARCHAR(2000) NOT NULL,
	[Source] NVARCHAR(2000) NOT NULL,
	[AppDomain] NVARCHAR(2000) NOT NULL,
	[TargetSite] NVARCHAR(2000) NOT NULL,
	[StackTrace] NVARCHAR(MAX) NOT NULL,
	[Data] NVARCHAR(MAX) NOT NULL,
	[Timestamp] DATETIME2(4) CONSTRAINT [DF_Exception_Timestamp] DEFAULT SYSUTCDATETIME() NOT NULL,
	CONSTRAINT [PK_Exception] PRIMARY KEY CLUSTERED ([ExceptionId])
)
WITH (DATA_COMPRESSION = PAGE)

GO

CREATE INDEX [IX_Exception_Timestamp] ON [lg].[Exception] ([Timestamp] DESC)

GO

CREATE INDEX [IX_Exception_Source] ON [lg].[Exception] ([Source])
