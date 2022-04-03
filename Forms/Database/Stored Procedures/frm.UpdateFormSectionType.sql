CREATE PROCEDURE [frm].[UpdateFormSectionType]
	@id INT,
	@title VARCHAR(256),
	@order SMALLINT,
	@hidden BIT,
	@timestamp DATETIME2(4) OUT
AS
BEGIN
	SET @timestamp = SYSUTCDATETIME();
	UPDATE [frm].[FormSectionType]
	SET 
		[Title] = @title, 
		[Order] = @order, 
		[Hidden] = @hidden, 
		[UpdateTimestamp] = @timestamp
	WHERE [FormSectionTypeId] = @id
	;
END