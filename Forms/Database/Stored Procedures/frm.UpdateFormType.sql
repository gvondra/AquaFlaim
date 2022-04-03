CREATE PROCEDURE [frm].[UpdateFormType]
	@id INT,
	@title VARCHAR(256),
	@timestamp DATETIME2(4) OUT
AS
BEGIN
	SET @timestamp = SYSUTCDATETIME();
	UPDATE [frm].[FormType]
	SET 
		[Title] = @title, 
		[UpdateTimestamp] = @timestamp
	WHERE [FormTypeId] = @id
	;	
END