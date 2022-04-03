CREATE PROCEDURE [frm].[CreateFormType]
	@id INT OUT,
	@title VARCHAR(256),
	@timestamp DATETIME2(4) OUT
AS
BEGIN
	SET @timestamp = SYSUTCDATETIME();
	INSERT INTO [frm].[FormType] ([Title], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@title, @timestamp, @timestamp)
	;
	SET @id = SCOPE_IDENTITY();
END