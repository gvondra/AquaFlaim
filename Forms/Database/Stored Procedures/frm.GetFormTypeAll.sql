CREATE PROCEDURE [frm].[GetFormTypeAll]
AS
SELECT [FormTypeId], [Title], [CreateTimestamp], [UpdateTimestamp]
FROM [frm].[FormType] WITH(READUNCOMMITTED)
ORDER BY [Title], [CreateTimestamp] DESC
;
