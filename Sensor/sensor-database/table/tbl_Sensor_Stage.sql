IF (OBJECT_ID(N'[dbo].[tbl_Sensor_Stage]', 'U') IS NULL)
BEGIN
  CREATE TABLE [dbo].[tbl_Sensor_Stage]
  ([dt_session] [smalldatetime] NULL
	,[nvc_source] [nvarchar](50) NULL
	,[nvc_dns] [nvarchar](100) NULL
	,[nvc_dnsstatus] [nvarchar](10) NULL
	,[nvc_ip] [nvarchar](20) NULL
	,[nvc_ipstatus] [nvarchar](10) NULL
	,[nvc_datacenter] [nvarchar](50) NULL
	,[nvc_datacentertag] [nvarchar](10) NULL
	,[i_port] [int] NULL
	,[i_latency] [int] NULL)
END
