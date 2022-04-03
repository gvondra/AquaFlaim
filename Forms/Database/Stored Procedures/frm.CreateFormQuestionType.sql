CREATE PROCEDURE [frm].[CreateFormQuestionType]
	@id INT OUT,
	@formTypeId INT,
	@formSectionTypeId INT,
	@code VARCHAR(128),
	@text VARCHAR(MAX),
	@responseType SMALLINT,
	@responseList VARCHAR(MAX),
	@responseMaxLength SMALLINT,
	@isRequired BIT,
	@responseValidationExpression VARCHAR(1024),
	@hidden BIT,
	@order SMALLINT,
	@timestamp DATETIME2(4) OUT
AS
BEGIN
	SET @timestamp = SYSUTCDATETIME();
	INSERT INTO [frm].[FormQuestionType] (
		[FormTypeId],[FormSectionTypeId],[Code],[Text],[ResponseType],[ResponseList],[ResponseMaxLength],[IsRequired],
		[ResponseValidationExpression],[Hidden],[Order],[CreateTimestamp],[UpdateTimestamp])
	VALUES (
		@formTypeId, @formSectionTypeId, @code, @text, @responseType, @responseList, @responseMaxLength, @isRequired,
		@responseValidationExpression, @hidden, @order, @timestamp, @timestamp
	);
	SET @id = SCOPE_IDENTITY();
END