CREATE PROCEDURE [dbo].[usp_Kiroku_KLogs_InsertAdd]
	(
		@dt_session [datetime],
		@dt_event [datetime],
		@ui_blockid [uniqueidentifier],
		@nvc_blockname [nvarchar](50),
		@nvc_logtype [nvarchar](15),
		@nvc_logdata [nvarchar](2000)
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN TRANSACTION

	INSERT INTO dbo.tbl_Kiroku_KLogs
	(
		dt_session, dt_event, ui_blockid, nvc_blockname, nvc_logtype, nvc_logdata
	)
	VALUES
	(
		@dt_session,
		@dt_event,
		@ui_blockid,
		@nvc_blockname,
		@nvc_logtype,
		@nvc_logdata

	)

	COMMIT

