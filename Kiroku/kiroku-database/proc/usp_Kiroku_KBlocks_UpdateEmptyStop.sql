CREATE PROCEDURE [dbo].[usp_Kiroku_KBlocks_UpdateEmptyStop]
	(
		@ui_instanceid [uniqueidentifier],
		@dt_blockstop [datetime]
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN TRANSACTION
		UPDATE dbo.tbl_Kiroku_KBlocks
		SET dt_blockstop = @dt_blockstop
		WHERE ui_instanceid = @ui_instanceid
		AND ui_blockid IS NULL
	COMMIT

