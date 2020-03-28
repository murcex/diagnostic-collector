USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Non-Static

CREATE TABLE [dbo].[tbl_SQLAvailability_Extract](
	[dt_session] [smalldatetime] NULL,
	[nvc_machine] [nvarchar](50) NOT NULL,
	[i_uptime] [datetime] NULL,
	[nvc_error] [nvarchar](1000) NULL,
 CONSTRAINT [PK_SQLAvailability_Extract] PRIMARY KEY CLUSTERED 
(
	[nvc_machine] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

