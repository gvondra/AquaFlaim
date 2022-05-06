CREATE PROCEDURE [cfg].[GetItem]
	@id UNIQUEIDENTIFIER
AS
SELECT TOP 1 [ItemId], [IsPublic], [Code], [Data], [CreateTimestamp], [UpdateTimestamp] 
FROM [cfg].[Item]
WHERE [ItemId] = @id
;