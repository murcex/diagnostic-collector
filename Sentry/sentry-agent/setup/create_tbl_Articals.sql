USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Static Table, create backup: SELECT * INTO [tbl_Sentry_Articals-bak] FROM [tbl_Sentry_Articals]
-- D_ROP TABLE [tbl_Sentry_Articals]

CREATE TABLE [dbo].[tbl_Sentry_Articals](
	[instance_name] [nvarchar](50) NULL,
	[replication_latency] [int] NULL,
	[database_capacity] [int] NULL
) ON [PRIMARY]

GO

