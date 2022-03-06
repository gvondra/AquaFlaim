﻿CREATE PROCEDURE [aut].[CreateUser]
	@id UNIQUEIDENTIFIER OUT,
	@refernceId VARCHAR(1024),
	@emailAddressId UNIQUEIDENTIFIER,
	@name VARCHAR(1024),
	@timestamp DATETIME2(4) OUT
AS
BEGIN
	SET @timestamp = SYSUTCDATETIME();
	SET @id = NEWID();
	INSERT INTO [aut].[User] ([UserId], [ReferenceId], [EmailAddressId], [Name], 
		[CreateTimestamp], [UpdateTimestamp])
	VALUES (@id, @refernceId, @emailAddressId, @name,
		@timestamp, @timestamp)
	;
END