USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	Create Report Snapshot
-- =============================================

CREATE PROCEDURE [dbo].[usp_DatabaseCapacity_Snapshot]

AS
BEGIN

-- Truncate Reporting Table
TRUNCATE TABLE [Sentry].[dbo].[tbl_DatabaseCapacity_Snapshot]

-- Delare Aggregation Variables
DECLARE @valueName NVARCHAR(50)

-- Setup Aggregation Cursor
DECLARE cur_AggReport CURSOR
FOR
SELECT RTRIM(item_value)
FROM [Sentry].[dbo].[tbl_Sentry_Configuration]
WHERE [item_name] = 'reporting'

-- Open Aggregation Cursor
OPEN cur_AggReport

FETCH NEXT
FROM cur_AggReport
INTO @valueName

-- Execute Aggregation Cursor
WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @CapAvailable DECIMAL(11, 2)
	DECLARE @CapAVG DECIMAL(11, 2)
	DECLARE @CapMIN DECIMAL(11, 2)
	DECLARE @CapMAX DECIMAL(11, 2)
	DECLARE @ThresholdAVG DECIMAL(11, 2)
	DECLARE @ThresholdMIN DECIMAL(11, 2)
	DECLARE @ThresholdMAX DECIMAL(11, 2)
	DECLARE @CapFixCount DECIMAL(11)
	DECLARE @ThresholdFixCount DECIMAL(11)
	--DECLARE @TotalFixCount DECIMAL(11)

	-- Capacity
	-- Set Capacity Available
	SELECT @CapAvailable = AVG([available_capacity])
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Growth]
	WHERE [database_name] LIKE '' + @valueName + '%'

	-- Set Capacity AVG
	SELECT @CapAVG = AVG([available_capacity_percentage])
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Growth]
	WHERE [database_name] LIKE '' + @valueName + '%'

	-- Set Capacity MIN
	SELECT @CapMIN = MIN([available_capacity_percentage])
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Growth]
	WHERE [database_name] LIKE '' + @valueName + '%'

	-- Set Capacity MAX
	SELECT @CapMAX = MAX([available_capacity_percentage])
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Growth]
	WHERE [database_name] LIKE '' + @valueName + '%'

	-- Threshold
	-- Set Threshold AVG
	SELECT @ThresholdAVG = AVG([capacity_threshold])
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Growth]
	WHERE [database_name] LIKE '' + @valueName + '%'

	-- Set Threshold MIN
	SELECT @ThresholdMIN = MIN([capacity_threshold])
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Growth]
	WHERE [database_name] LIKE '' + @valueName + '%'

	-- Set Threshold MAX
	SELECT @ThresholdMAX = MAX([capacity_threshold])
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Growth]
	WHERE [database_name] LIKE '' + @valueName + '%'

	-- Fix Count
	-- Set Cap Fix Items
	SELECT @CapFixCount = COUNT([available_capacity_percentage])
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Growth]
	WHERE [database_name] LIKE '' + @valueName + '%'
		AND [available_capacity_percentage] < 10

	-- Set Threshold Fix Count
	SELECT @ThresholdFixCount = COUNT([capacity_threshold])
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Growth]
	WHERE [database_name] LIKE '' + @valueName + '%'
		AND [capacity_threshold] < 60

	-- Display Colletion
	--SELECT @valueName AS [targetSet]
	--	,@CapAvailable AS [availableCapacity]
	--	,@CapAVG AS [AVGCapacity]
	--	,@CapMIN AS [MINCapacity]
	--	,@CapMAX AS [MAXCapacity]
	--	,@ThresholdAVG AS [AVGThreshold]
	--	,@ThresholdMIN AS [MINThreshold]
	--	,@ThresholdMAX AS [MAXThreshold]
	--	,@CapFixCount AS [FIXCapacity]
	--	,@ThresholdFixCount AS [FIXThreshold]
	
	-- Insert Collection Into Reporting Table
	INSERT INTO [dbo].[tbl_DatabaseCapacity_Snapshot] (
		[Database]
		,[CapAvailable]
		,[CapAVG]
		,[CapMIN]
		,[CapMAX]
		,[ThresholdAVG]
		,[ThresholdMIN]
		,[ThresholdMAX]
		,[CapFixCount]
		,[ThresholdFixCount]
		)
	VALUES (
	    @valueName
		,@CapAvailable
		,@CapAVG
		,@CapMIN
		,@CapMAX
		,@ThresholdAVG
		,@ThresholdMIN
		,@ThresholdMAX
		,@CapFixCount
		,@ThresholdFixCount
		)

	FETCH NEXT
	FROM cur_AggReport
	INTO @valueName
END

-- Cursor Aggregation Clean-up
CLOSE cur_AggReport

DEALLOCATE cur_AggReport

END

GO


