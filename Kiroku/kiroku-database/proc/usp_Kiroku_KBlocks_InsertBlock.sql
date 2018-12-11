CREATE PROCEDURE [dbo].[usp_Kiroku_KBlocks_InsertBlock]
	(
		@ui_instanceid [uniqueidentifier],
		@ui_blockid [uniqueidentifier],
		@nvc_blockname [nchar](50),
		@dt_blockstart [datetime],
		@dt_blockstop [datetime]
		--@dt_duration [datetime]
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN TRANSACTION

	INSERT INTO dbo.tbl_Kiroku_KBlocks
	(
		ui_instanceid,
		ui_blockid, 
		nvc_blockname, 
		dt_blockstart,
		dt_blockstop
		--dt_duration
	)
	VALUES
	(
		@ui_instanceid,
		@ui_blockid,
		@nvc_blockname,
		@dt_blockstart,
		@dt_blockstop
		--@dt_duration

	)

	COMMIT