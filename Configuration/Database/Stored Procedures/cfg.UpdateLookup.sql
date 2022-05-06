CREATE PROCEDURE [cfg].[UpdateLookup]
	@id UNIQUEIDENTIFIER,
	@isPublic BIT = 0,
	@code VARCHAR(512),
	@data VARCHAR(MAX),
	@timestamp DATETIME2(4) OUT
AS
BEGIN
	SET @timestamp = SYSUTCDATETIME();
	UPDATE [cfg].[Lookup] 
	SET 
		[IsPublic] = @isPublic,
		[Code] = @code,
		[Data] = @data,
		[UpdateTimestamp] = @timestamp
	WHERE [LookupId] = @id
	;
END