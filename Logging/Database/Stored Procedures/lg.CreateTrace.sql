CREATE PROCEDURE [lg].[CreateTrace]
	@id UNIQUEIDENTIFIER OUT,
	@eventCode VARCHAR(200),
	@message NVARCHAR(2000),
	@data NVARCHAR(MAX),
	@timestamp DATETIME2(4)
AS
BEGIN
	SET @id = NEWID();
	INSERT INTO [lg].[Trace] ([TraceId],[EventCode], [Message], [Data], [Timestamp]) 
	VALUES (@id, @eventCode, @message, @data, @timestamp)
	;
END