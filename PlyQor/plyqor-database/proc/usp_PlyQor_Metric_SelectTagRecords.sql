CREATE PROCEDURE [dbo].[usp_PlyQor_Metric_SelectTagRecords]

AS

BEGIN

INSERT INTO [tbl_PlyQor_Metric] ([dt_timestamp], [nvc_container], [nvc_metrictype], [nvc_metric], [i_value])
SELECT
DATEADD(DAY, 0, DATEDIFF(DAY, 0, GETUTCDATE()))
,[nvc_container]
,'Records'
,'Tag'
,COUNT([nvc_id])
FROM [dbo].[tbl_PlyQor_Data]
GROUP BY [nvc_container]

END
