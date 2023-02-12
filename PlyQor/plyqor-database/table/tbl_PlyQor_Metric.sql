CREATE TABLE [dbo].[tbl_PlyQor_Metric](
	[dt_timestamp] [smalldatetime] NOT NULL,
	[nvc_container] [nvarchar](20) NOT NULL,
	[nvc_metrictype] [nvarchar](20) NOT NULL,
	[nvc_metric] [nvarchar](20) NOT NULL,
	[i_value] [int] NOT NULL
) ON [PRIMARY]
