CREATE VIEW [dbo].[vw_Sensor_Base_Latency]

AS

SELECT 
[dt_session] AS [Session]
,[nvc_dns] AS [DNS]
,NULL AS [Data Center Tag]
,NULL AS [Count]
,[nvc_source] + '-' + [nvc_datacentertag] AS [Trace Route]
,[i_latency] AS [Latency]
FROM [dbo].[tbl_Sensor_Stage]
WHERE [dt_session] > DATEADD(day,-7,GETDATE())