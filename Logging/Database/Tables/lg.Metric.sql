CREATE TABLE [lg].[Metric]
(
	[MetricId] UNIQUEIDENTIFIER NOT NULL,
	[EventCode] VARCHAR(200) NOT NULL,
	[Magnitude] FLOAT NULL, 
	[Status] VARCHAR(256) NOT NULL,
	[Data] NVARCHAR(MAX) NOT NULL,
	[Timestamp] DATETIME2(4) CONSTRAINT [DF_Metric_Timestamp] DEFAULT SYSUTCDATETIME() NOT NULL,
	CONSTRAINT [PK_Metric] PRIMARY KEY CLUSTERED ([MetricId])
)
WITH (DATA_COMPRESSION = PAGE)

GO

CREATE INDEX [IX_Metric_Timestamp] ON [lg].[Metric] ([Timestamp] DESC)

GO

CREATE INDEX [IX_Metric_EventCode] ON [lg].[Metric] ([EventCode])
