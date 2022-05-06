CREATE PROCEDURE [cfg].[GetLookup]
	@id UNIQUEIDENTIFIER
AS
SELECT TOP 1 [LookupId], [IsPublic], [Code], [Data], [CreateTimestamp], [UpdateTimestamp] 
FROM [cfg].[Lookup]
WHERE [LookupId] = @id
;