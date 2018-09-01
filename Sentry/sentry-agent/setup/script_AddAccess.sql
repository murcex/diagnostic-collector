CREATE LOGIN [DOMAIN\user]
FROM WINDOWS WITH DEFAULT_DATABASE = [master]
	,DEFAULT_LANGUAGE = [us_english]
GO

EXEC master..sp_addsrvrolemember @loginame = N'DOMAIN\user'
	,@rolename = N'sysadmin'
GO