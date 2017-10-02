USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_DatabaseCapacity_Aggregation] (
	[execution_runtime] [smalldatetime] NULL
	,[artical_name] [nvarchar](50) NULL
	,[database_name] [nvarchar](50) NULL
	,[aggregated_allocated_capacity] [decimal](11, 2) NULL
	,[aggregated_utilized_capacity] [decimal](11, 2) NULL
	,[aggregated_available_capacity] [decimal](11, 2) NULL
	,[available_capacity_percentage] [decimal](11, 2) NULL
	) ON [PRIMARY]
GO