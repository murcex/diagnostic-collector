USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReplicationLatency_Staging](
	[replication_latency_runtime] [smalldatetime] NULL,
	[replication_instance_name] [nvarchar](100) NULL,
	[replication_stream_name] [nvarchar](100) NOT NULL,
	[replication_latency] [nvarchar](50) NULL,
 CONSTRAINT [PK_ReplicationLatency_Staging] PRIMARY KEY CLUSTERED 
(
	[replication_stream_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


