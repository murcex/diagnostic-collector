CREATE VIEW [dbo].[vw_Sensor_Base_Region]

AS

SELECT
[dt_session] AS [Session]
,[nvc_dns] AS [DNS]
,[nvc_datacentertag] AS [Data Center Tag]
,COUNT(*) AS [Count]
,NULL AS [Trace Route]
,NULL AS [Latency]
FROM [dbo].[tbl_Sensor_Stage]
WHERE [dt_session] > DATEADD(day,-7,GETDATE())
GROUP BY [dt_session], [nvc_dns], [nvc_datacentertag]