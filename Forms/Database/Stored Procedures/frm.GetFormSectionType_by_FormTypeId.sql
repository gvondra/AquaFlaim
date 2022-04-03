CREATE PROCEDURE [frm].[GetFormSectionType_by_FormTypeId]
	@formTypeId INT
AS
SELECT [FormSectionTypeId], [FormTypeId], [Title], [Order], [Hidden], [CreateTimestamp], [UpdateTimestamp]
FROM [frm].[FormSectionType]
WHERE [FormTypeId] = @formTypeId
ORDER BY [Order]
;