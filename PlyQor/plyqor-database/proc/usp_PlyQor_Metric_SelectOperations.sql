CREATE PROCEDURE [dbo].[usp_PlyQor_Metric_SelectOperations]

AS

BEGIN

INSERT INTO [tbl_PlyQor_Metric] ([dt_timestamp], [nvc_container], [nvc_metrictype], [nvc_metric], [i_value])
SELECT DISTINCT
DATEADD(DAY, 0, DATEDIFF(DAY, 0, [dt_timestamp]))
,[nvc_container]
,'Operations'
,[nvc_operation]
,COUNT([nvc_id])
FROM [dbo].[tbl_PlyQor_Trace]
WHERE DATEADD(DAY, 0, DATEDIFF(DAY, 0, [dt_timestamp])) = DATEADD(DAY, -1, CAST(GETUTCDATE() AS DATE))
GROUP BY DATEADD(DAY, 0, DATEDIFF(DAY, 0, [dt_timestamp])), [nvc_container], [nvc_operation]

END
