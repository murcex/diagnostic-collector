CREATE TABLE [dbo].[tbl_Kiroku_KMetrics](
	[dt_session] [datetime] NULL,
	[dt_event] [datetime] NULL,
	[ui_blockid] [uniqueidentifier] NULL,
	[nvc_metricname] [nvarchar](100) NULL,
	[nvc_metrictype] [nvarchar](10) NULL,
	[nvc_metricvalue] [nvarchar](10) NULL
) ON [PRIMARY]