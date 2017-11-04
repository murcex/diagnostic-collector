USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description: Sentry Database Capacity Extraction
-- =============================================

CREATE PROCEDURE [dbo].[usp_DatabaseCapacity_Extract]

AS
BEGIN

	-- Execution Logging - Start
	-- %%% Add Execution Log Stored Procedure
	-- Archive Staging Table
	-- %%% Add Archiving Staging Table
	-- Truncate Tables
	TRUNCATE TABLE Sentry.dbo.tbl_DatabaseCapacity_Staging
	TRUNCATE TABLE Sentry.dbo.tbl_DatabaseCapacity_Aggregation

	-- Delare Instance Variables
	DECLARE @InstanceName VARCHAR(MAX)
	DECLARE @cmd VARCHAR(1000)
	DECLARE @ExecutionTime SMALLDATETIME

	SELECT @ExecutionTime = GETDATE();

	-- Setup Instance Cursor
	DECLARE Cursor_DatabaseCapacity CURSOR
	FOR
	SELECT instance_name
	FROM Sentry.dbo.tbl_Sentry_Articals
	WHERE database_capacity = 1

	-- Open Instance Cursor
	OPEN Cursor_DatabaseCapacity

	FETCH NEXT
	FROM Cursor_DatabaseCapacity
	INTO @InstanceName

	-- Loop Instance Cursor
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @cmd = @InstanceName + '.master.dbo.Sentry_DatabaseCapacity'

		BEGIN TRY
			INSERT INTO Sentry.dbo.tbl_DatabaseCapacity_Staging
			EXEC (@cmd)
		END TRY

		-- Try-Catch
		BEGIN CATCH
			INSERT INTO Sentry.dbo.tbl_Sentry_Log_Error
			VALUES (
				@Executiontime
				,'database_capacity'
				,ERROR_MESSAGE()
				)
		END CATCH

		-- Reload Instance Cursor
		FETCH NEXT
		FROM Cursor_DatabaseCapacity
		INTO @InstanceName
	END

	-- Clean-up Instance Cursor
	CLOSE Cursor_DatabaseCapacity

	DEALLOCATE Cursor_DatabaseCapacity

	INSERT INTO [Sentry].[dbo].[tbl_DatabaseCapacity_Aggregation]
	SELECT DISTINCT execution_runtime
		,[artical_name]
		,[database_name]
		,SUM([allocated_capacity]) AS aggregated_allocated_capacity
		,SUM([utilized_capacity]) AS aggregated_utilized_capacity
		,SUM([available_capacity]) AS aggregated_available_capacity
		,CONVERT(DECIMAL(11, 2), (SUM([available_capacity]) / SUM([allocated_capacity])) * 100) AS available_capacity_percentage
	FROM [Sentry].[dbo].[tbl_DatabaseCapacity_Staging]
	WHERE file_type = 0
	GROUP BY [execution_runtime]
		,[artical_name]
		,[database_name]

END

GO