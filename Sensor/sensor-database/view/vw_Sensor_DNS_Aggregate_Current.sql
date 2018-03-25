CREATE VIEW [dbo].[vw_Sensor_DNS_Aggregate_Current]

AS

SELECT
DISTINCT([nvc_dns]) AS [DNS]
,[nvc_datacenter] AS [Data Center]
,[nvc_datacentertag] AS [Data Center Tag]
FROM [dbo].[tbl_Sensor_DNS_Stage]
WHERE [dt_session] = (SELECT TOP 1 [dt_session] FROM [dbo].[tbl_Sensor_DNS_Stage] ORDER BY [dt_session] DESC)
