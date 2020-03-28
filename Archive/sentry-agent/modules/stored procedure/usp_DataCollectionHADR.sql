USE [master]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	Local Database Capacity
-- =============================================

CREATE PROCEDURE [dbo].[Sentry_DatabaseCapacity]

AS
BEGIN

-- Delcare Var
DECLARE @command VARCHAR(5000)
DECLARE @serverName VARCHAR(5000)
DECLARE @isHadrEnabled VARCHAR(5000)

-- Collect Hadr Status
SELECT @isHadrEnabled = 'SELECT SERVERPROPERTY(''IsHadrEnabled'')'

-- Hadr Check and ServerName set
IF SERVERPROPERTY('IsHadrEnabled') = 1
	SELECT @Servername = dns_name
	FROM sys.availability_group_listeners
ELSE
	SELECT @Servername = @@servername

-- Collect Database Capacity Data
SELECT @command = '
USE [' + '?' + '] 
SELECT   
	GETDATE()
	, ''' + @ServerName + ''' AS ServerName
	, ' + '''' + '?' + '''' + ' AS DatabaseName   
	, name
	, type
	, convert(decimal(12,2),round(a.size/128.000,2)) as FileSizeMB 
	, convert(decimal(12,2),round(fileproperty(a.name,' + '''' + 'SpaceUsed' + '''' + ')/128.000,2)) as SpaceUsedMB 
	, convert(decimal(12,2),round((a.size-fileproperty(a.name,' + '''' + 'SpaceUsed' + '''' + '))/128.000,2)) as FreeSpaceMB, 
	CAST(100 * (CAST (((a.size/128.0 -CAST(FILEPROPERTY(a.name,' + '''' + 'SpaceUsed' + '''' + ' ) AS int)/128.0)/(a.size/128.0)) AS decimal(4,2))) AS varchar(8)) + ' + '''' + '%' + '''' + ' AS FreeSpacePct 
FROM sys.database_files a'

EXEC sp_MSForEachDB @command

END

GO


