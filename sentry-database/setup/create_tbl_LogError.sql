USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Non-Static Table

CREATE TABLE [dbo].[tbl_Sentry_Log_Error](
	[log_entry_runtime] [smalldatetime] NULL,
	[module_name] [nvarchar](500) NULL,
	[error_data] [nvarchar](500) NULL
) ON [PRIMARY]

GO

