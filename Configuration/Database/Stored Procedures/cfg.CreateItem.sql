CREATE PROCEDURE [cfg].[CreateItem]
	@id UNIQUEIDENTIFIER OUT,
	@isPublic BIT = 0,
	@code VARCHAR(512),
	@data VARCHAR(MAX),
	@timestamp DATETIME2(4) OUT
AS
BEGIN
	SET @timestamp = SYSUTCDATETIME();
	SET @id = NEWID();
	INSERT INTO [cfg].[Item] ([ItemId], [IsPublic], [Code], [Data], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @isPublic, @code, @data, @timestamp, @timestamp)
	;
END