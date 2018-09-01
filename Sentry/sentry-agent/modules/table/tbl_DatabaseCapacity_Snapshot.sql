USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_DatabaseCapacity_Snapshot](
	[Database] [nvarchar](50) NULL,
	[CapAvailable] [decimal](18, 0) NULL,
	[CapAVG] [decimal](18, 0) NULL,
	[CapMIN] [decimal](18, 0) NULL,
	[CapMAX] [decimal](18, 0) NULL,
	[ThresholdAVG] [decimal](18, 0) NULL,
	[ThresholdMIN] [decimal](18, 0) NULL,
	[ThresholdMAX] [decimal](18, 0) NULL,
	[CapFixCount] [decimal](18, 0) NULL,
	[ThresholdFixCount] [decimal](18, 0) NULL
) ON [PRIMARY]

GO


