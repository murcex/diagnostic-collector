CREATE PROCEDURE [dbo].[usp_Kiroku_KBlocks_InsertStart]
	(
		@ui_instanceid [uniqueidentifier],
		@ui_blockid [uniqueidentifier],
		@nvc_blockname [nchar](50),
		@dt_blockstart [datetime]
		--@dt_blockend [datetime],
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
		dt_blockstart 
		--dt_blockend, 
		--dt_duration
	)
	VALUES
	(
		@ui_instanceid,
		@ui_blockid,
		@nvc_blockname,
		@dt_blockstart
		--@dt_blockend,
		--@dt_duration

	)

	COMMIT