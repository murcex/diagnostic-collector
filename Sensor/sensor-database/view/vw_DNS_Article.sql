--IF (OBJECT_ID(N'[dbo].[vw_DNS_Article]', 'V') IS NULL)
--BEGIN
  CREATE VIEW [dbo].[vw_DNS_Article] AS
  SELECT TOP 5000
  [dt_session] AS [Session]
  ,[nvc_source] AS [Source]
  ,[nvc_datacenter] AS [Data Center]
  ,[nvc_datacentertag] AS [Data Center Tag]
  ,[nvc_dns] AS [DNS]
  ,[nvc_ip] AS [IPAddress]
  ,[nvc_status] AS [Status]
  FROM [dbo].[tbl_DNS_Stage]
  ORDER BY [dt_session] DESC
--END
