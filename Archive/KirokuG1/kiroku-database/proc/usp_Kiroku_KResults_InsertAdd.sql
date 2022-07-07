CREATE PROCEDURE [dbo].[usp_Kiroku_KResults_InsertAdd]
	(
		@dt_session [datetime],
		@dt_event [datetime],
		@nvc_resulttype [nvarchar](15),
		@ui_resultid [uniqueidentifier],
		@i_result [int],
		@nvc_resultdata [nvarchar](100)
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN TRANSACTION

	INSERT INTO dbo.tbl_Kiroku_KResults
	(
		dt_session, 
		dt_event, 
		nvc_resulttype, 
		ui_resultid, 
		i_result, 
		nvc_resultdata
	)
	VALUES
	(
		@dt_session,
		@dt_event,
		@nvc_resulttype,
		@ui_resultid,
		@i_result,
		@nvc_resultdata

	)

	COMMIT