CREATE PROCEDURE [dbo].[usp_PlyQor_Metric_SelectAvailability]

AS

BEGIN

INSERT INTO [tbl_PlyQor_Metric] ([dt_timestamp], [nvc_container], [nvc_metrictype], [nvc_metric], [i_value])
SELECT
DATEADD(DAY, 0, DATEDIFF(DAY, 0, [dt_timestamp]))
,[nvc_container]
,'Availability'
,'Availability'
,(
(

(
SELECT COUNT([nvc_id])
FROM [dbo].[tbl_PlyQor_Trace]
WHERE  [dt_timestamp] > DATEADD(DAY, -1, GETUTCDATE())
AND [nvc_status] = 'True'
)

/

(
SELECT COUNT([nvc_id])
FROM [dbo].[tbl_PlyQor_Trace]
WHERE [dt_timestamp] > DATEADD(DAY, -1, GETUTCDATE())
)

) * 100

) --AS [Availability]
FROM [dbo].[tbl_PlyQor_Trace]
WHERE DATEADD(DAY,0, DATEDIFF(DAY, 0, [dt_timestamp])) = DATEADD(DAY, -1, CAST(GETUTCDATE() AS DATE))
GROUP BY DATEADD(DAY,0, DATEDIFF(DAY, 0, [dt_timestamp])), [nvc_container]

END
