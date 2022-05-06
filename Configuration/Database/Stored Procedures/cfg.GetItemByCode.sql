CREATE PROCEDURE [cfg].[GetItemByCode]
	@code VARCHAR(512)
AS
SELECT TOP 1 [ItemId], [IsPublic], [Code], [Data], [CreateTimestamp], [UpdateTimestamp] 
FROM [cfg].[Item]
WHERE [Code] = @code
;