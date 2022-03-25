CREATE PROCEDURE [lg].[GetMetricEvents]
AS
SELECT [EventCode]
FROM [lg].[Metric] WITH(READUNCOMMITTED)
GROUP BY [EventCode]
;