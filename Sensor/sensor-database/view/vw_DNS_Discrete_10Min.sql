CREATE VIEW [dbo].[vw_DNS_Discrete_10Min] AS
SELECT
[nvc_dns] AS [DNS]
,[nvc_datacentertag] AS [Data Center Tag]
,COUNT(*) AS [Count]
FROM [dbo].[tbl_DNS_Stage]
WHERE [dt_session] > DATEADD(minute,-11,GETDATE())
GROUP BY [nvc_dns], [nvc_datacentertag]
