USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_DatabaseCapacity_Growth] (
	[execution_runtime] [smalldatetime] NULL
	,[artical_name] [nvarchar](50) NULL
	,[database_name] [nvarchar](50) NULL
	,[allocated_capacity] [decimal](11, 2) NULL
	,[utilized_capacity] [decimal](11, 2) NULL
	,[available_capacity] [decimal](11, 2) NULL
	,[available_capacity_percentage] [decimal](11, 2) NULL
	,[average_capacity_growth] [decimal](11, 2) NULL
	,[capacity_threshold] [decimal](5, 0) NULL
	) ON [PRIMARY]
GO