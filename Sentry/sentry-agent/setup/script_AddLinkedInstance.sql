-- Add Linked Server
sp_addlinkedserver @server = 'serverName'

-- Update Server Options
USE [master]
GO

EXEC master.dbo.sp_serveroption @server = N'serverName'
	,@optname = N'remote proc transaction promotion'
	,@optvalue = N'false'
GO


