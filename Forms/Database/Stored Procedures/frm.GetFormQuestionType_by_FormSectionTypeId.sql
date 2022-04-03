CREATE PROCEDURE [frm].[GetFormQuestionType_by_FormSectionTypeId]
	@formSectionTypeId INT
AS
SELECT [FormQuestionTypeId], [FormTypeId],[FormSectionTypeId],[Code],[Text],[ResponseType],[ResponseList],[ResponseMaxLength],[IsRequired],
	[ResponseValidationExpression],[Hidden],[Order],[CreateTimestamp],[UpdateTimestamp]
FROM [frm].[FormQuestionType]
WHERE [FormSectionTypeId] = @formSectionTypeId
ORDER BY [Order]
;