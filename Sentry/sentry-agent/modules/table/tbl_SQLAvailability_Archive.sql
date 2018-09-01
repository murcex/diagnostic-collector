USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Static Table

CREATE TABLE [dbo].[tbl_SQLAvailability_Archive](
	[dt_session] [smalldatetime] NULL,
	[nvc_machine] [nvarchar](50) NOT NULL,
	[i_statuscode] [int] NULL,
	[nvc_status] [nvarchar](50) NULL,
	[i_uptime] [nvarchar](50) NULL,
	[nvc_active] [nvarchar](50) NULL
) ON [PRIMARY]

GO

