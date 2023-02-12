CREATE PROCEDURE [dbo].[usp_PlyQor_Metric_SelectTransactions]

AS

BEGIN

INSERT INTO [tbl_PlyQor_Metric] ([dt_timestamp], [nvc_container], [nvc_metrictype], [nvc_metric], [i_value])
SELECT
DATEADD(DAY, 0, DATEDIFF(DAY, 0, GETUTCDATE()))
,[nvc_container]
,'Transactions'
,'Transactions'
,COUNT([nvc_id])
FROM [dbo].[tbl_PlyQor_Trace]
GROUP BY [nvc_container]

END
