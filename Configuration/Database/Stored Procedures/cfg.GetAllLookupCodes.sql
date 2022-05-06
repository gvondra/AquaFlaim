CREATE PROCEDURE [cfg].[GetAllLookupCodes]
	@includePrivate BIT = 0
AS
SELECT [Code]
FROM [cfg].[Lookup] WITH(READUNCOMMITTED)
WHERE [IsPublic] = 1 OR @includePrivate = 1
ORDER BY [Code]
;