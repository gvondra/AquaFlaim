CREATE PROCEDURE [cfg].[CreateLookup]
	@id UNIQUEIDENTIFIER OUT,
	@isPublic BIT = 0,
	@code VARCHAR(512),
	@data VARCHAR(MAX),
	@timestamp DATETIME2(4) OUT
AS
BEGIN
	SET @timestamp = SYSUTCDATETIME();
	SET @id = NEWID();
	INSERT INTO [cfg].[Lookup] ([LookupId], [IsPublic], [Code], [Data], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @isPublic, @code, @data, @timestamp, @timestamp)
	;
END