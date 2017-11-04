USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	Sentry Database Capacity Threshold
-- =============================================

CREATE PROCEDURE [dbo].[usp_DatabaseCapacity_Threshold]

AS
BEGIN

	-- Truncate Tables
	TRUNCATE TABLE Sentry.dbo.tbl_DatabaseCapacity_Growth

	-- Delare Threshold Variables
	DECLARE @articalName NVARCHAR(50)
	DECLARE @databaseName NVARCHAR(50)

	-- Setup Threshold Cursor
	DECLARE cur_CapacityGrowth CURSOR
	FOR
	SELECT artical_name
		,database_name
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Aggregation]

	-- Open Threshold Cursor
	OPEN cur_CapacityGrowth

	FETCH NEXT
	FROM cur_CapacityGrowth
	INTO @articalName
		,@databaseName

	-- Execute Threshold Cursor
	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE @CapAVG DECIMAL(11, 2)
		DECLARE @CapAllocated DECIMAL(11, 2)
		DECLARE @CapUtilized DECIMAL(11, 2)
		DECLARE @CapAvailable DECIMAL(11, 2)
		DECLARE @CapAvailablePre DECIMAL(11, 2)
		DECLARE @CapThreshold DECIMAL(18)
		
		-- Build Threshold CTE
		WITH Capacity_SUM([Total Capacity Growth], [Day]) AS (
				SELECT SUM([capacity_growth]) AS [Total Capacity Growth]
					,DATEPART(day, execution_runtime) AS [Day]
				FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Archive]
				WHERE artical_name = @articalName
					AND database_name = @databaseName
					AND execution_runtime > DATEADD(WEEK, - 1, GETDATE())
				GROUP BY DATEPART(day, execution_runtime)
				)

		-- Set Capacity AVG
		SELECT @CapAVG = AVG([Total Capacity Growth])
		FROM Capacity_SUM

		-- Set Capacity Available
		SELECT TOP 1 @CapAllocated = aggregated_allocated_capacity
			,@CapUtilized = aggregated_utilized_capacity
			,@CapAvailable = aggregated_available_capacity
			,@CapAvailablePre = available_capacity_percentage
		FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Archive]
		WHERE artical_name = @articalName
			AND database_name = @databaseName
		ORDER BY execution_runtime DESC

		-- # Calclate Days-to-Fill
		-- Check Growth <= 0
		IF @CapAVG <= 0
		BEGIN
			SET @CapThreshold = 9999
		END
		ELSE
		BEGIN
			-- Check Threshold > 9999 Days 
			SET @CapThreshold = CONVERT(DECIMAL(18), @CapAvailable / @CapAVG)

			IF @CapThreshold > 9999
			BEGIN
				SET @CapThreshold = 9999
			END
		END

		-- Insert Record
		INSERT INTO [Sentry].[dbo].[tbl_DatabaseCapacity_Growth]
		SELECT GETDATE()
			,@articalName
			,@databaseName
			,@CapAllocated
			,@CapUtilized
			,@CapAvailable
			,@CapAvailablePre
			,@CapAVG
			,@CapThreshold

		FETCH NEXT
		FROM cur_CapacityGrowth
		INTO @articalName
			,@databaseName
	END

	-- Cursor Aggregation Clean-up
	CLOSE cur_CapacityGrowth

	DEALLOCATE cur_CapacityGrowth

END

GO