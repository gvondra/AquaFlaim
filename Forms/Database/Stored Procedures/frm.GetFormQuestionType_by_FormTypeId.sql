CREATE PROCEDURE [frm].[GetFormQuestionType_by_FormTypeId]
	@formTypeId INT
AS
SELECT [FormQuestionTypeId], [FormTypeId],[FormSectionTypeId],[Code],[Text],[ResponseType],[ResponseList],[ResponseMaxLength],[IsRequired],
	[ResponseValidationExpression],[Hidden],[Order],[CreateTimestamp],[UpdateTimestamp]
FROM [frm].[FormQuestionType]
WHERE [FormTypeId] = @formTypeId
ORDER BY [Order]
;