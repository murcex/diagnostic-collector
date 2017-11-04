USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description: SQL Availability Extraction
-- =============================================

CREATE PROCEDURE [dbo].[usp_SQLAvailability_Extract]
AS

-- Clearing Staging Table
TRUNCATE TABLE Sentry.dbo.tbl_SQLAvailability_Extract

-- Delare Variables
DECLARE @Target VARCHAR(MAX)
DECLARE @Query VARCHAR(MAX)
DECLARE @ExecutionTime SMALLDATETIME

-- Set Execution Tme
SELECT @ExecutionTime = GETDATE();

-- Setup Cursor
DECLARE Cursor_SQLAvailability CURSOR
FOR
SELECT instance_name
FROM Sentry.dbo.Sentry_Articals
WHERE sqlavailability > 0

-- Open Cursor
OPEN Cursor_SQLAvailability

FETCH NEXT
FROM Cursor_SQLAvailability
INTO @Target

-- Loop Cursor
WHILE @@FETCH_STATUS = 0
BEGIN
	-- Build Query
	SET @Query = 'INSERT INTO Sentry.dbo.tbl_SQLAvailability_Extract SELECT NULL, ''' + @Target + ''', sqlserver_start_time, NULL FROM ' + @Target + '.master.sys.dm_os_sys_info'

	-- Execute Query
	BEGIN TRY
		EXEC (@Query)
	END TRY

	-- Try-Catch
	BEGIN CATCH
			INSERT INTO Sentry.dbo.tbl_SQLAvailability_Extract
		VALUES (
			@Executiontime
			,@Target
			,NULL
			,ERROR_MESSAGE()
			)

		INSERT INTO Sentry.dbo.Sentry_Log_Error
		VALUES (
			@Executiontime
			,'SQLAvailability'
			,ERROR_MESSAGE()
			)
	END CATCH

	-- Reload Cursor
	FETCH NEXT
	FROM Cursor_SQLAvailability
	INTO @Target
END

-- Cursor Clean-up
CLOSE Cursor_SQLAvailability

DEALLOCATE Cursor_SQLAvailability

-- Add Execution Time
UPDATE Sentry.dbo.tbl_SQLAvailability_Extract
SET dt_session = @Executiontime


GO

