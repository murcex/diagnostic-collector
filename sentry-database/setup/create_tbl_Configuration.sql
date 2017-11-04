USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Static Table, create backup: SELECT * INTO [tbl_Sentry_Configuration-bak] FROM [tbl_Sentry_Configuration]
-- D_ROP TABLE [tbl_Sentry_Configuration]

CREATE TABLE [dbo].[tbl_Sentry_Configuration](
	[module] [nvarchar](50) NULL,
	[item_type] [nvarchar](50) NULL,
	[item_name] [nvarchar](50) NULL,
	[item_value] [nvarchar](4000) NULL
) ON [PRIMARY]

GO

