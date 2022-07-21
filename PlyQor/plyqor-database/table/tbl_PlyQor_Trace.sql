CREATE TABLE [dbo].[tbl_PlyQor_Trace](
	[dt_timestamp] [smalldatetime] NOT NULL,
	[nvc_container] [nvarchar](20) NOT NULL,
	[nvc_id] [nvarchar](50) NOT NULL,
	[nvc_operation] [nvarchar](20) NOT NULL,
	[nvc_code] [nvarchar](10) NOT NULL,
	[nvc_status] [nvarchar](20) NOT NULL,
	[i_duration] [int] NOT NULL
) ON [PRIMARY]
