USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_ReplicationLatency_Staging] (
	[replication_latency_runtime] [smalldatetime] NULL
	,[replication_instance_name] [nvarchar](100) NULL
	,[replication_stream_name] [nvarchar](100) NULL
	,[replication_latency] [nvarchar](50) NULL
	) ON [PRIMARY]
GO