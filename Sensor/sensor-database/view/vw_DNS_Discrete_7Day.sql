CREATE VIEW [dbo].[vw_DNS_Discrete_7Day] AS
SELECT
[dt_session] AS [Session]
,[nvc_dns] AS [DNS]
,[nvc_datacentertag] AS [Data Center Tag]
,COUNT(*) AS [Count]
FROM [dbo].[tbl_DNS_Stage]
WHERE [dt_session] > DATEADD(day,-7,GETDATE())
GROUP BY [dt_session], [nvc_dns], [nvc_datacentertag]
