CREATE PROCEDURE [frm].[GetFormType]
	@id INT
AS
SELECT TOP 1 [FormTypeId], [Title], [CreateTimestamp], [UpdateTimestamp]
FROM [frm].[FormType] 
WHERE [FormTypeId] = @id
;
