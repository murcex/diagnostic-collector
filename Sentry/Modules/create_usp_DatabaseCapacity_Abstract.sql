USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	Sentry Database Capacity Abstraction
-- =============================================

CREATE PROCEDURE [dbo].[usp_DatabaseCapacity_Abstract]

AS
BEGIN

-- Delare Aggregation Variables
DECLARE @AggregateUpdate_Instance VARCHAR(MAX)
DECLARE @AggregateUpdate_Database VARCHAR(MAX)

-- Setup Aggregation Cursor
DECLARE cur_AggregateUpdate CURSOR FOR 
SELECT artical_name, database_name
FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Aggregation]

-- Open Aggregation Cursor
OPEN cur_AggregateUpdate
FETCH NEXT FROM cur_AggregateUpdate INTO @AggregateUpdate_Instance, @AggregateUpdate_Database

-- Execute Aggregation Cursor
WHILE @@FETCH_STATUS = 0
BEGIN

		-- Delare Aggergation Load Variables
		DECLARE @StagingUtilizedCapacity decimal(11, 2)
		DECLARE @AggreagateRunTime smalldatetime
		DECLARE @AggregatedUtilizedCapacity decimal(11, 2)
		--DECLARE @test4 smalldatetime

		-- Set Staging Utilized Capacity
		SET @StagingUtilizedCapacity = (SELECT TOP 1
		[aggregated_utilized_capacity]
		FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Aggregation]
		WHERE [artical_name] = '' + @AggregateUpdate_Instance + ''
		AND [database_name] = '' + @AggregateUpdate_Database + ''
		ORDER BY [execution_runtime] DESC)

		-- Set Aggreagate Runtime
		SET @AggreagateRunTime = (SELECT TOP 1
		[execution_runtime]
		FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Archive]
		WHERE [artical_name] = '' + @AggregateUpdate_Instance + ''
		AND [database_name] = '' + @AggregateUpdate_Database + ''
		ORDER BY [execution_runtime] DESC)

		-- Set Aggreagate Utilized Capacity
		SET @AggregatedUtilizedCapacity = (SELECT TOP 1
		[aggregated_utilized_capacity]
		FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Archive]
		WHERE [artical_name] = '' + @AggregateUpdate_Instance + ''
		AND [database_name] = '' + @AggregateUpdate_Database + ''
		ORDER BY [execution_runtime] DESC)
		
		-- Set Pervious Aggreagate Day ?
		--SET @test4 = (SELECT TOP 1
		--[execution_runtime]
		--FROM [Sentry].[dbo].[DatabaseCapacity_Aggregation]
		--WHERE [artical_name] = '' + @AggregateUpdate_Instance + ''
		--AND [database_name] = '' + @AggregateUpdate_Database + ''
		--ORDER BY [execution_runtime] DESC)
		
		--DATEDIFF(datepart,startdate,enddate)
		--SELECT DATEDIFF(day,@CapacityRuntime,@test4)

		-- Archive Aggreagation Data
		INSERT INTO [Sentry].[dbo].[tbl_DatabaseCapacity_Archive]
		SELECT 
				 [execution_runtime]
				,[artical_name]
				,[database_name]
				,[aggregated_allocated_capacity]
				,[aggregated_utilized_capacity]
				,[aggregated_available_capacity]
				,[available_capacity_percentage]
				,(@StagingUtilizedCapacity-@AggregatedUtilizedCapacity)
				,@AggreagateRunTime
				,NULL
		FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Aggregation]
		WHERE [artical_name] = ''+ @AggregateUpdate_Instance +''
		AND [database_name] = ''+ @AggregateUpdate_Database +''
	
-- Reload Aggregation Cursor
FETCH NEXT FROM cur_AggregateUpdate INTO @AggregateUpdate_Instance, @AggregateUpdate_Database

END

-- Cursor Aggregation Clean-up
CLOSE cur_AggregateUpdate
DEALLOCATE cur_AggregateUpdate

END

GO

