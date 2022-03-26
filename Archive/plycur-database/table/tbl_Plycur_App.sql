CREATE TABLE [dbo].[tbl_Plycur_App]
(
	[dt_created] [smalldatetime] NULL,
	[dt_updated] [smalldatetime] NULL,
	[i_status] [int] NULL,
	[nvc_key] [nvarchar](50) NOT NULL,
	[nvc_value] [nvarchar](250) NULL
) 
ON [PRIMARY]