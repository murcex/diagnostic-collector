CREATE PROCEDURE [dbo].[usp_Kiroku_KMetrics_InsertAdd]
	(
		@dt_session [datetime],
		@dt_event [datetime],
		@ui_blockid [uniqueidentifier],
		@nvc_metricname [nvarchar](100),
		@nvc_metrictype [nvarchar](10),
		@nvc_metricvalue [nvarchar](10)
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN TRANSACTION

	INSERT INTO dbo.tbl_Kiroku_KMetrics
	(
		dt_session, 
		dt_event, 
		ui_blockid, 
		nvc_metricname, 
		nvc_metrictype, 
		nvc_metricvalue
	)
	VALUES
	(
		@dt_session,
		@dt_event,
		@ui_blockid,
		@nvc_metricname,
		@nvc_metrictype,
		@nvc_metricvalue

	)

	COMMIT