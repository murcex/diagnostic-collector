USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tbl_DatabaseCapacity_Staging] (
	[execution_runtime] [smalldatetime] NULL
	,[artical_name] [varchar](100) NULL
	,[database_name] [varchar](100) NULL
	,[file_name] [nvarchar](520) NULL
	,[file_type] [int] NULL
	,[allocated_capacity] [decimal](10, 2) NULL
	,[utilized_capacity] [decimal](10, 2) NULL
	,[available_capacity] [decimal](10, 2) NULL
	,[available_capacity_percentage] [varchar](8) NULL
	) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO