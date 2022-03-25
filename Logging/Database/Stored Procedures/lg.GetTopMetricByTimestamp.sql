CREATE PROCEDURE [lg].[GetTopMetricByTimestamp]
	@maxTimestamp DATETIME2(4)
AS
SELECT TOP 250 [MetricId], [EventCode], [Magnitude], [Status], [Data], [Timestamp]
FROM [lg].[Metric]
WHERE [Timestamp] in (
	SELECT TOP 50 [Timestamp]
	FROM [lg].[Metric]
	WHERE [Timestamp] < @maxTimestamp
	GROUP BY [Timestamp]
)
ORDER BY [Timestamp] DESC, [MetricId]
;