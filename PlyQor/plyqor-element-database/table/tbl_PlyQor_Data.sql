CREATE TABLE [dbo].[tbl_PlyQor_Data](
	[dt_timestamp] [smalldatetime] NOT NULL,
	[nvc_container] [nvarchar](20) NOT NULL,
	[nvc_id] [nvarchar](50) NOT NULL,
	[nvc_data] [nvarchar](max) NOT NULL,
	CONSTRAINT PK_Data_ConatinerId PRIMARY KEY (nvc_container,nvc_id)   
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]