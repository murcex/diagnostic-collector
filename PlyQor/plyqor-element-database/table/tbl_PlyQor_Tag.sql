CREATE TABLE [dbo].[tbl_PlyQor_Tag](
	[dt_timestamp] [smalldatetime] NOT NULL,
	[nvc_container] [nvarchar](20) NOT NULL,
	[nvc_id] [nvarchar](50) NOT NULL,
	[nvc_data] [nvarchar](50) NOT NULL,
	CONSTRAINT PK_Tag_ConatinerIdTag PRIMARY KEY (nvc_container,nvc_id,nvc_data)
) ON [PRIMARY]