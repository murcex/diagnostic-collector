CREATE TABLE [dbo].[tbl_Kiroku_KBlocks](
	[ui_instanceid] [uniqueidentifier] NULL,
	[ui_blockid] [uniqueidentifier] NULL,
	[nvc_blockname] [nchar](50) NULL,
	[dt_blockstart] [datetime] NULL,
	[dt_blockstop] [datetime] NULL,
	[i_duration]  AS (datediff(millisecond,[dt_blockstart],[dt_blockstop]))
) ON [PRIMARY]