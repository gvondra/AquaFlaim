CREATE PROCEDURE [lg].[CreateMetric]
	@id UNIQUEIDENTIFIER OUT,
	@eventCode VARCHAR(200),
	@magnitude FLOAT,
	@status VARCHAR(256),
	@data NVARCHAR(MAX),
	@timestamp DATETIME2(4)
AS
BEGIN
	SET @id = NEWID();
	INSERT INTO [lg].[Metric] ([MetricId], [EventCode], [Magnitude], [Status], [Data], [Timestamp])
	VALUES (@id, @eventCode, @magnitude, @status, @data, @timestamp)
	;
END