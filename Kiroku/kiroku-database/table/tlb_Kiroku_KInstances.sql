CREATE TABLE [dbo].[tlb_Kiroku_KInstances](
	[ui_instanceid] [uniqueidentifier] NULL,
	[dt_instancestart] [datetime] NULL,
	[dt_instancestop] [datetime] NULL,
	[i_duration]  AS (datediff(millisecond,[dt_instancestart],[dt_instancestop])),
	[ui_applicationid] [uniqueidentifier] NULL,
	[nvc_klogversion] [nvarchar](20) NULL
) ON [PRIMARY]