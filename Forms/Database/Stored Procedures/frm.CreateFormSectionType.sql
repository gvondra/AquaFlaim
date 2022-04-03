CREATE PROCEDURE [frm].[CreateFormSectionType]
	@id INT OUT,
	@formTypeId INT,
	@title VARCHAR(256),
	@order SMALLINT,
	@hidden BIT,
	@timestamp DATETIME2(4) OUT
AS
BEGIN
	SET @timestamp = SYSUTCDATETIME();
	INSERT INTO [frm].[FormSectionType] ([FormTypeId], [Title], [Order], [Hidden], [CreateTimestamp], [UpdateTimestamp])
	VALUES (@formTypeId, @title, @order, @hidden, @timestamp, @timestamp)
	;
	SET @id = SCOPE_IDENTITY();
END