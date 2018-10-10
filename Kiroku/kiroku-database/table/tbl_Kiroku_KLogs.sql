CREATE TABLE [dbo].[tbl_Kiroku_KLogs](
	[dt_session] [datetime] NULL,
	[dt_event] [datetime] NULL,
	[ui_blockid] [uniqueidentifier] NULL,
	[nvc_blockname] [nvarchar](50) NULL,
	[nvc_logtype] [nvarchar](15) NULL,
	[nvc_logdata] [nvarchar](2000) NULL
) ON [PRIMARY]