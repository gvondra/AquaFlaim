CREATE PROCEDURE [cfg].[GetLookupByCode]
	@code VARCHAR(512)
AS
SELECT TOP 1 [LookupId], [IsPublic], [Code], [Data], [CreateTimestamp], [UpdateTimestamp] 
FROM [cfg].[Lookup]
WHERE [Code] = @code
;