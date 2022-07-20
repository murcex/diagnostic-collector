CREATE TABLE [dbo].[tbl_Kiroku_KResults](
	[dt_session] [datetime] NULL,
	[dt_event] [datetime] NULL,
	[nvc_resulttype] [nvarchar](15) NULL,
	[ui_resultid] [uniqueidentifier] NULL,
	[i_result] [int] NULL,
	[nvc_resultdata] [nvarchar](100) NULL
) ON [PRIMARY]