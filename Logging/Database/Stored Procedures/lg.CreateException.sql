CREATE PROCEDURE [lg].[CreateException]
	@id UNIQUEIDENTIFIER OUT,
	@parentExceptionId UNIQUEIDENTIFIER,
	@message NVARCHAR(2000),
	@typeName NVARCHAR(2000),
	@source NVARCHAR(2000),
	@appDomain NVARCHAR(2000),
	@targetSite NVARCHAR(2000),
	@stackTrace NVARCHAR(MAX),
	@data NVARCHAR(MAX),
	@timestamp DATETIME2(4)
AS
BEGIN
	SET @id = NEWID();
	INSERT INTO [lg].[Exception] ([ExceptionId], [ParentExceptionId], [Message], [TypeName], [Source], [AppDomain], [TargetSite], [StackTrace], [Data], [Timestamp])
	VALUES (@id, @parentExceptionId, @message, @typeName, @source, @appDomain, @targetSite, @stackTrace, @data, @timestamp)
	;
END