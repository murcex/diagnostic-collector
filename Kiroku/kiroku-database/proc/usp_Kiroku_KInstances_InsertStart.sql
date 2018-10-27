CREATE PROCEDURE [dbo].[usp_Kiroku_KInstances_InsertStart]
	(
		@ui_instanceid [uniqueidentifier],
		@dt_instancestart [datetime],
		--@dt_instancestop [datetime],
		--@dt_duration [datetime],
		@ui_applicationid [uniqueidentifier],
		@nvc_klogversion [nvarchar](20)
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	INSERT INTO dbo.tbl_Kiroku_KInstances
	(
		ui_instanceid, dt_instancestart, 
		--dt_instancestop, 
		--dt_duration, 
		ui_applicationid, nvc_klogversion
	)
	VALUES
	(
		@ui_instanceid,
		@dt_instancestart,
		--@dt_instancestop,
		--@dt_duration,
		@ui_applicationid,
		@nvc_klogversion
	)

