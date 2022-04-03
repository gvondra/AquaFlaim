CREATE PROCEDURE [frm].[UpdateFormQuestionType]
	@id INT,
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
	UPDATE [frm].[FormQuestionType] 
	set 
		[FormSectionTypeId] = @formSectionTypeId,
		[Code] = @code,
		[Text] = @text,
		[ResponseType] = @responseType,
		[ResponseList] = @responseList,
		[ResponseMaxLength] = @responseMaxLength,
		[IsRequired] = @isRequired,
		[ResponseValidationExpression] = @responseValidationExpression,
		[Hidden] = @hidden,
		[Order] = @order,
		[UpdateTimestamp] = @timestamp
	WHERE [FormQuestionTypeId] = @id
	;
END