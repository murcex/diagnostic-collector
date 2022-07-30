CREATE TABLE [dbo].[tbl_PlyQor_Metrics](
	[dt_timestamp] [smalldatetime] NOT NULL,
	[nvc_container] [nvarchar](20) NOT NULL,
	[i_type] [int] NOT NULL,
	[nvc_key] [nvarchar](50) NOT NULL,
	[i_value] [int] NOT NULL
) ON [PRIMARY]
