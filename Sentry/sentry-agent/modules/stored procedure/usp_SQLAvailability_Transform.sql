USE [Sentry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	Sentry SQL Availability Transformation
-- =============================================

CREATE PROCEDURE [dbo].[usp_SQLAvailability_Transform]

AS
BEGIN

-- Archive Load Table
INSERT INTO Sentry.dbo.tbl_SQLAvailability_Archive
SELECT dt_session,nvc_machine,i_statuscode,nvc_status,i_uptime,nvc_active
FROM Sentry.dbo.tbl_SQLAvailability_Load

-- Truncate Load Table
TRUNCATE TABLE [Sentry].[dbo].[tbl_SQLAvailability_Load]

-- Delare Aggregation Variables
DECLARE @SQLAvailability_session SMALLDATETIME
DECLARE @SQLAvailability_machine VARCHAR(MAX)
DECLARE @SQLAvailability_uptime SMALLDATETIME
DECLARE @SQLAvailability_error VARCHAR(MAX)

-- Setup Aggregation Cursor
DECLARE cur_SQLAvailabilityTransform CURSOR FOR 
SELECT [dt_session], [nvc_machine], [i_uptime], [nvc_error]
FROM [Sentry].[dbo].[tbl_SQLAvailability_Extract]

-- Open Aggregation Cursor
OPEN cur_SQLAvailabilityTransform
FETCH NEXT FROM cur_SQLAvailabilityTransform INTO @SQLAvailability_session, @SQLAvailability_machine, @SQLAvailability_uptime, @SQLAvailability_error

-- Execute Aggregation Cursor
WHILE @@FETCH_STATUS = 0
BEGIN

DECLARE @SQLAvailability_datediff INT
DECLARE @SQLAvailability_statuscode INT
DECLARE @SQLAvailability_status VARCHAR(MAX)

-- if uptime = NULL then set status,statuscode,uptime = OFFLINE,0,-1
IF (@SQLAvailability_error IS NOT NULL)
	BEGIN
		SELECT @SQLAvailability_statuscode = 0, @SQLAvailability_status = 'OFFLINE', @SQLAvailability_datediff = -1
	END
ELSE
	BEGIN
		SELECT @SQLAvailability_datediff = (SELECT DATEDIFF(MINUTE,@SQLAvailability_uptime,@SQLAvailability_session))

		IF (@SQLAvailability_datediff > -1 AND @SQLAvailability_datediff <= 30)
			BEGIN
				SELECT @SQLAvailability_statuscode = 1, @SQLAvailability_status = 'CRITICAL'
			END
		ELSE
			BEGIN
				IF (@SQLAvailability_datediff > 30 AND @SQLAvailability_datediff <= 120)
					BEGIN
						SELECT @SQLAvailability_statuscode = 2, @SQLAvailability_status = 'CAUTION'
					END
				ELSE
					BEGIN
						SELECT @SQLAvailability_statuscode = 3, @SQLAvailability_status = 'ONLINE'
					END
			END
	END

INSERT INTO [dbo].[tbl_SQLAvailability_Load]
    VALUES
        (@SQLAvailability_session
        ,@SQLAvailability_machine
        ,@SQLAvailability_statuscode
        ,@SQLAvailability_status
        ,@SQLAvailability_datediff
        ,'Primary')
	
-- Reload Aggregation Cursor
FETCH NEXT FROM cur_SQLAvailabilityTransform INTO @SQLAvailability_session, @SQLAvailability_machine, @SQLAvailability_uptime, @SQLAvailability_error

END

-- Cursor Aggregation Clean-up
CLOSE cur_SQLAvailabilityTransform
DEALLOCATE cur_SQLAvailabilityTransform

END


GO

