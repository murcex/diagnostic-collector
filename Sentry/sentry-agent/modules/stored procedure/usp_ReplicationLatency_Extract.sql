USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	Replication Latency Extraction
-- =============================================

CREATE PROCEDURE [dbo].[usp_ReplicationLatency_Extract]
AS
-- Archive Staging Table
INSERT INTO Sentry.dbo.tbl_ReplicationLatency_Archive
SELECT replication_latency_runtime
	,replication_instance_name
	,replication_stream_name
	,replication_latency
FROM Sentry.dbo.tbl_ReplicationLatency_Staging

-- Clearing Staging Table
TRUNCATE TABLE Sentry.dbo.tbl_ReplicationLatency_Staging

-- Delare Variables
DECLARE @Target VARCHAR(MAX)
DECLARE @Query VARCHAR(MAX)
DECLARE @ExecutionTime SMALLDATETIME

SELECT @ExecutionTime = GETDATE();

-- Setup Cursor
DECLARE Cursor_ReplicationLatency CURSOR
FOR
SELECT instance_name
FROM Sentry.dbo.tbl_Sentry_Articals
WHERE replication_latency > 0

-- Open Cursor
OPEN Cursor_ReplicationLatency

FETCH NEXT
FROM Cursor_ReplicationLatency
INTO @Target

-- Loop Cursor
WHILE @@FETCH_STATUS = 0
BEGIN
	-- Build Query
	SET @Query = 'INSERT INTO Sentry.dbo.tbl_ReplicationLatency_Staging SELECT NULL, ''' + @Target + ''', instance_name, ROUND(cntr_value/60000,0) FROM ' + @Target + '.master.sys.dm_os_performance_counters WHERE counter_name LIKE ''%Dist:Delivery Latency%'''

	-- Execute Query
	BEGIN TRY
		EXEC (@Query)
	END TRY

	-- Try-Catch
	BEGIN CATCH
		INSERT INTO Sentry.dbo.tbl_Sentry_Log_Error
		VALUES (
			@Executiontime
			,'replication_latency'
			,ERROR_MESSAGE()
			)
	END CATCH

	-- Reload Cursor
	FETCH NEXT
	FROM Cursor_ReplicationLatency
	INTO @Target
END

-- Cursor Clean-up
CLOSE Cursor_ReplicationLatency

DEALLOCATE Cursor_ReplicationLatency

-- Add Execution Time
UPDATE Sentry.dbo.tbl_ReplicationLatency_Staging
SET replication_latency_runtime = @Executiontime
	-- %%% v-robg @ 1/27/16 | Soft Delete, Hard Delete Pending
	-- Clearing SCOM Table
	--TRUNCATE TABLE Sentry.dbo.ReplicationLatency_SCOM
	-- Update SCOM Table
	--INSERT INTO Sentry.dbo.ReplicationLatency_SCOM
	--SELECT replication_latency_runtime, replication_instance_name, replication_stream_name, replication_latency
	--FROM Sentry.dbo.ReplicationLatency_Staging
	--SELECT A.[replication_latency_runtime], A.[replication_instance_name], A.[replication_stream_name], A.[replication_latency]
	--FROM [dbo].[ReplicationLatency_Staging] A
	--JOIN [dbo].[Sentry_Articals] B
	--ON B.[instance_name] = A.[replication_instance_name]
	--WHERE B.[replication_latency] = 1
GO