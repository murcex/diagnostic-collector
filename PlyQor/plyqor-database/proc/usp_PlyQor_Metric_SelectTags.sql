CREATE PROCEDURE [dbo].[usp_PlyQor_Metric_SelectTags]

AS

BEGIN

INSERT INTO [tbl_PlyQor_Metric] ([dt_timestamp], [nvc_container], [nvc_metrictype], [nvc_metric], [i_value])
SELECT
DATEADD(DAY, 0, DATEDIFF(DAY, 0, GETUTCDATE()))
,[nvc_container]
,'Tags'
,[nvc_data]
,COUNT([nvc_data])
FROM [dbo].[tbl_PlyQor_Tag]
GROUP BY [nvc_container], [nvc_data]

END
