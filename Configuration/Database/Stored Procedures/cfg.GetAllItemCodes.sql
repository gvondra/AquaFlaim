CREATE PROCEDURE [cfg].[GetAllItemCodes]
	@includePrivate BIT = 0
AS
SELECT [Code]
FROM [cfg].[Item] WITH(READUNCOMMITTED)
WHERE [IsPublic] = 1 OR @includePrivate = 1
ORDER BY [Code]
;