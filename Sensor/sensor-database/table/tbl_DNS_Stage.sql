IF (OBJECT_ID(N'[dbo].[tbl_DNS_Stage]', 'U') IS NULL)
BEGIN
  CREATE TABLE [dbo].[tbl_DNS_Stage]
  ([dt_session] [smalldatetime] NULL,
	[nvc_source] [nvarchar](50) NULL,
	[nvc_datacenter] [nvarchar](50) NULL,
	[nvc_datacentertag] [nvarchar](10) NULL,
	[nvc_dns] [nvarchar](100) NULL,
	[nvc_ip] [nvarchar](20) NULL,
	[nvc_status] [nvarchar](10) NULL)
END
