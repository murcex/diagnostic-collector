CREATE VIEW [dbo].[vw_Sensor_DNS_Article]

AS

SELECT
[dt_session] AS [Session]
,[nvc_source] AS [Source]
,[nvc_datacenter] AS [Data Center]
,[nvc_datacentertag] AS [Data Center Tag]
,[nvc_dns] AS [DNS]
,[nvc_ip] AS [IPAddress]
,[nvc_status] AS [Status]
FROM [dbo].[tbl_Sensor_DNS_Stage]
